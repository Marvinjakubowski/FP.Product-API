using System.IO;
using System.Reflection;
using System;
using FP_Product_API.Interfaces.Repository;
using FP_Product_API.Interfaces.Services;
using FP_Product_API.Serializer;
using FP_Product_API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace FP_Product_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            DependencyIncectionInit(builder);
            SwaggerGenInitialze(builder);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("v1/swagger.json", "Product-API V1");
                });
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }

        private static void DependencyIncectionInit(WebApplicationBuilder builder)
        {
            // Add services to the container.
            builder.Services.AddTransient<IProductDataService, ProductDataService>();
            builder.Services.AddTransient<IProductStatisticService, ProductStatisticService>();
            builder.Services.AddTransient<IProductDataRepository, ProductDataRepository>();
        }

        private static void SwaggerGenInitialze(WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Flaschenpost Product-API",
                    Description = "Product-API to generate product-statistics."
                });
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
        }

    }
}