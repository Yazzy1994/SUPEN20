using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SUPEN20DB.DbContexts;
using SUPEN20DB.Entites;
using SUPEN20DB.Seeders;
using SystemAPI.Services;

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
         
            services.AddTransient<Seeder>();
            services.AddScoped(typeof(IRespository<>), typeof(SUPEN20Respository<>)); 
            services.AddControllers();
            services.AddSwaggerGen(setupAction => // This is the middleware.Here are we calling the SwaggerGenerator and this accept a setupAction to set it up.
            {
                setupAction.SwaggerDoc( //This add a OpenAPI Specification.
                    "Supen20OpenAPISpecification", 
                    new Microsoft.OpenApi.Models.OpenApiInfo() { 
                            Title= "System API",
                            Version = "1"
                    });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
