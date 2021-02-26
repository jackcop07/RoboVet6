using System.Diagnostics.CodeAnalysis;
using System.Text;
using AutoMapper;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using RoboVet6.Data.DbContext;
using RoboVet6.DataAccess.Common.Interfaces;
using RoboVet6.DataAccess.Repositories;
using RoboVet6.Service.Common.Interfaces;
using RoboVet6.Service.Services;
using Serilog;

namespace RoboVet6.API
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
                {
                    o.Authority = "https://localhost:5000";
                    o.RequireHttpsMetadata = false;
                        o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                        {
                            ValidateAudience = true,
                            ValidAudience = "https://localhost:5000/resources",
                            

                        };
                });



            //Entity Framework
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("DatabaseConnection"));

            //Add service
            services.AddScoped<IClientsService, ClientsService>();
            services.AddScoped<IClientRepository, ClientRepository>();

            services.AddScoped<IAnimalsService, AnimalsService>();
            services.AddScoped<IAnimalRepository, AnimalRepository>();

            //Swagger
            services.AddSwaggerGen();

            //Auto mapper
            services.AddAutoMapper(typeof(RoboVet6.Service.Common.Mappings.Mapper));


            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder
                        .WithOrigins("https://localhost:44384")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                    });
            });

            services.AddControllers();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "RoboVet 6 V1");
                c.RoutePrefix = string.Empty;
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected error occurred. Try again later.");
                    });
                });
            }
            //app.UseCors("AllowSpecificOrigin");
            app.UseCors(x => x
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()
            );


            app.UseHttpsRedirection();
            app.UseSerilogRequestLogging();

            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
