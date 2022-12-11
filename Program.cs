using AutoMapper;
using disclone_api.DTOs;
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

namespace disclone_api
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var builder = WebApplication.CreateBuilder(args);
            Settings = builder.Configuration;
            if(Environment.GetEnvironmentVariable("ENCRYPTION_KEY") != null){
                Settings["EncryptionKey"] = Environment.GetEnvironmentVariable("ENCRYPTION_KEY");
            }
            if(Environment.GetEnvironmentVariable("DB_PASSWORD") != null){
                Settings["DBPassword"] = Environment.GetEnvironmentVariable("DB_PASSWORD");
            }

            //Mapper
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();

            builder.Services.AddSingleton(mapper);
            builder.Services.AddMvc();
            builder.Services.AddControllers()
                .AddJsonOptions(options => 
                {
                    options.JsonSerializerOptions.WriteIndented = true;
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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Settings["EncryptionKey"] )),
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateLifetime = false,
                    RequireExpirationTime = false,
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuerSigningKey = true
                };
                
            });


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options => {
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
            });
            builder.Services.RegisterServices();
            var conStrBuilder = new NpgsqlConnectionStringBuilder(builder.Configuration.GetConnectionString("local"));
            conStrBuilder.Password = Settings["DBPassword"];
            builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(conStrBuilder.ConnectionString));
            

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "frontendOrigin",
                    policy  =>
                    {
                        policy.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod();
                    });
            });
            
            builder.Services.AddScoped<ITokenBuilder, TokenBuilder>();

            builder.Services.AddLogging(x => x.AddFile("logs/log.txt")).BuildServiceProvider();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("frontendOrigin");
            // Microsoft Things: https://stackoverflow.com/questions/57998262/why-is-claimtypes-nameidentifier-not-mapping-to-sub
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            app.UseAuthorization();
            app.MapControllers();
            app.UseCors("frontendOrigin");
            app.Run();

        }
        

        public static IConfiguration Settings { get; private set; }
    }
}

