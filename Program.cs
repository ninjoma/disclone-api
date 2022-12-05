using AutoMapper;
using disclone_api.DTOs;
using disclone_api.Services;
using disclone_api.utils;
using Npgsql;
using Microsoft.EntityFrameworkCore;

namespace disclone_api
{
    class Program
    {
        static void Main(string[] args)
        {
     

            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.

            //Mapper
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();

            builder.Services.AddSingleton(mapper);
            builder.Services.AddMvc();
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.RegisterServices();
            var conStrBuilder = new NpgsqlConnectionStringBuilder(builder.Configuration.GetConnectionString("local"));
            conStrBuilder.Password = builder.Configuration["DBPassword"];
            builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(conStrBuilder.ConnectionString));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "frontendOrigin",
                    policy  =>
                    {
                        policy.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod();
                    });
            });


            Settings = builder.Configuration;

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("frontendOrigin");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();

        }
        

        public static IConfiguration Settings { get; private set; }
    }
}

