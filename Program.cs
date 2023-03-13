using AutoMapper;
using disclone_api.Services;
using disclone_api.utils;
using Npgsql;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using System.Reflection;
using disclone_api.DTO;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Sentry;
using disclone_api.Hubs;

namespace disclone_api
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.WebHost.UseSentry(o =>
                {
                    o.Dsn = "https://45554c0222a94b118c6d7ca101b3c04d@o4504822339010560.ingest.sentry.io/4504822340845568";
                    o.TracesSampleRate = 1.0;
                }
            );
            // Allow multiple appsettings environments (local, prod, dev)...

            builder.Configuration
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment}.json", optional: true)
                .AddEnvironmentVariables();

            Settings = builder.Configuration;

            

            // Load Encryption Key and Password from Environment. (Docker Configuration)
            if(Environment.GetEnvironmentVariable("ENCRYPTION_KEY") != null){
                Settings["EncryptionKey"] = Environment.GetEnvironmentVariable("ENCRYPTION_KEY");
            }
            
            if(Environment.GetEnvironmentVariable("DB_PASSWORD") != null){
                Settings["DBPassword"] = Environment.GetEnvironmentVariable("DB_PASSWORD");
            }

            if(Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") != null){
                Settings["DB_CONNECTION_STRING"] = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            }

            if(Environment.GetEnvironmentVariable("FRONTEND_LOCATION") != null){
                Settings["FRONTEND_LOCATION"] = Environment.GetEnvironmentVariable("FRONTEND_LOCATION");
            }

            // Mapper
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();

            builder.Services.AddSingleton(mapper);
            builder.Services.AddMvc();
            builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true)
            .AddNewtonsoftJson(options => {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                
            });

            builder.Services.AddAuthentication(x => 
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => 
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;

                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Settings["EncryptionKey"])),
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateLifetime = false,
                    RequireExpirationTime = false,
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuerSigningKey = true
                };
            });
            builder.Services.AddRouting(options => options.LowercaseUrls = true);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options => {
                // JWT Auth
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    Description = "Jwt Token",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {jwtSecurityScheme , Array.Empty<String>() }
                });

                // Documentation (Swagger Docs)
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Disclone API",
                    Description = "Backend del mejor clon de discord, Disclone"
                });
                options.AddSignalRSwaggerGen();
                var filePath = Path.Combine(System.AppContext.BaseDirectory, "disclone-api.xml");
                options.IncludeXmlComments(filePath);
            });
            builder.Services.RegisterServices();

            // Support for environment based connection strings
            var connStr = "";
            if(Settings["DB_CONNECTION_STRING"] != null){
                connStr = Settings["DB_CONNECTION_STRING"];
            } else {
                connStr = builder.Configuration.GetConnectionString("local");
            }
            Console.WriteLine("EncryptionKey: " + Settings["EncryptionKey"]);
            Console.WriteLine("DBPassword: " + Settings["DBPassword"]);
            Console.WriteLine("Connection String found: " + connStr);
            var conStrBuilder = new NpgsqlConnectionStringBuilder(connStr);
            Console.WriteLine("conStrBuilder.ConnectionString: " + conStrBuilder.ConnectionString);
            conStrBuilder.Password = Settings["DBPassword"];
            builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(conStrBuilder.ConnectionString));
            
            var origin = "http://localhost:5173";
            if(Settings["FRONTEND_LOCATION"] != null){
                origin = Settings["FRONTEND_LOCATION"];
            }
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "frontendOrigin",
                    policy  =>
                    {
                        policy.WithOrigins(origin).AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                    });
            });
            
            builder.Services.AddScoped<ITokenBuilder, TokenBuilder>();

            builder.Services.AddLogging(x => x.AddFile("logs/log.txt")).BuildServiceProvider();
            builder.Services.AddSignalR();

            var app = builder.Build();  
            app.UseSentryTracing();

            // Run migrations
            using(var scope = app.Services.CreateScope()) {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<DataContext>();
                if(context.Database.GetPendingMigrations().Any()){
                    context.Database.Migrate();
                }
            }


            if (app.Environment.IsDevelopment() || Environment.GetEnvironmentVariable("USE_SWAGGER") == "true")
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            // Microsoft Things: https://stackoverflow.com/questions/57998262/why-is-claimtypes-nameidentifier-not-mapping-to-sub
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            app.UseAuthorization();
            
            app.MapControllers();
            app.UseCors("frontendOrigin");

            app.MapHub<EventHub>("/hub");

            app.Run();

        }
        

        public static IConfiguration Settings { get; private set; }
    }
}

