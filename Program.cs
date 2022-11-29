using AutoMapper;
using disclone_api.DTOs;
using disclone_api.Services;
using disclone_api.Services.UserServices;
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
            builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("local")));

            var app = builder.Build();

            

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();


            app.UseAuthorization();

            app.MapControllers();

            app.Run();

        }
    }
}

