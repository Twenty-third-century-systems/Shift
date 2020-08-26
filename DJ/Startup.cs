using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DJ.DataModels;
using LinqToDB.AspNet;
using LinqToDB.AspNet.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace DJ {
    public class Startup {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            services.AddSwaggerGen(x =>
                x.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Entity Reg API",
                    Description = "Client API",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Brighton Kofu",
                        Email = "brightonkofu@outlook.com",
                        Url = new Uri("https://kofu.brighton"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                }));
                
                            
            
            services.AddLinqToDbContext<EachDB>((provider, options) =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("EachDatabase"))
                    .UseDefaultLogging(provider);
            });
            
            services.AddLinqToDbContext<PoleDB>((provider, options) =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("PoleDatabase"))
                    .UseDefaultLogging(provider);
            });
            
            services.AddLinqToDbContext<ShwaDB>((provider, options) =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ShwaDatabase"))
                    .UseDefaultLogging(provider);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }                       

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}