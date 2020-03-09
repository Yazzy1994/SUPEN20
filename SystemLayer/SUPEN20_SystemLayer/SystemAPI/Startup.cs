using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SUPEN20DB.DbContexts;
using SystemAPI.Services;
using SUPEN20DB.Entites;
using SUPEN20DB.Seeder;

namespace SystemAPI
{
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
            
            services.AddDbContext<SUPEN20DbContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("DatabaseConnection"));

            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); //AddAutoMapper is define in AutoMapper. This method allows me to input a set of assemblies. It is these assemblies that will automatically get scanned for profiles that contain mapping configurations.  

        
         
            services.AddTransient<DataSeeder>();
            services.AddScoped(typeof(IRespository<Order>), typeof(OrderRespository)); //Able to use the respository interface, We need to configure dependency injection.This Scoped service takes our interface and our implementation with DbContext

            services.AddControllers()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

                
            services.AddSwaggerGen(setupAction => // This is the middleware.Here are we calling the SwaggerGenerator and this accept a setupAction to set it up.
            {
                setupAction.SwaggerDoc( //This add a OpenAPI Specification.
                    "Supen20OpenAPISpecification", 
                    new Microsoft.OpenApi.Models.OpenApiInfo() { 
                            Title= "System API",
                            Version = "1"
                    });
            });

            //services.AddDbContext<SUPEN20DbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DatabaseConnection")));
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            } 
            else
            { //This returns a generic message to the consumer when a 500 fault happens. This only works when the enviroment in ASP.NET Core is set to Production. 
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async contex =>
                    {
                        contex.Response.StatusCode = 500;
                        await contex.Response.WriteAsync("An unexpected fault happened. Try again later!");
                    });
                }); 
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger(); //This is the Swashbuckle request pipeline. 

            app.UseSwaggerUI(setupAction => //This is the swaggerUI middleware to the request pipeline. 
            {
                setupAction.SwaggerEndpoint( //This is the endpoint where SwaggerUI can find the OpenAPI specification generetad by SwaggerGen. 
                 "/swagger/Supen20OpenAPISpecification/swagger.json",
                    "System API");
                
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints => 
            {
                endpoints.MapControllers();
            });
        }
    }
}
