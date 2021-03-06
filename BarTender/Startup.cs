using System;
using System.Collections.Generic;
using AutoMapper;
using BarTender.Background;
using BarTender.Models;
using Fridge.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TurnTable.ExternalServices.MutualExclusion;
using TurnTable.ExternalServices.NameSearch;
using TurnTable.ExternalServices.Outputs;
using TurnTable.ExternalServices.Payments;
using TurnTable.ExternalServices.Paynow;
using TurnTable.ExternalServices.PrivateEntity;
using TurnTable.ExternalServices.Values;

namespace BarTender {
    public class Startup {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MainDatabaseContext>(o =>
            {
                o.UseSqlServer(Configuration.GetConnectionString("BigDb"));
            });
            
            
            services.AddDbContext<PaymentsDatabaseContext>(o =>
            {
                o.UseSqlServer(Configuration.GetConnectionString("pytDb"));
            });

            services.AddControllers(o =>
                {
                    o.Filters.Add(new AuthorizeFilter());

                    o.ReturnHttpNotAcceptable = true;
                })
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "https://localhost:5001";

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "scope1");
                });
            });

            services.AddSwaggerGen(x =>
                x.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v2.1",
                    Title = "API",
                    Description = "Internal",
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

            services.AddAutoMapper(typeof(Program));

            // Custom services

            services.Configure<List<ServicesForNameSearchSelection>>(Configuration.GetSection("Services"));

            services.Configure<List<ReasonForSearchForNameSearchSelection>>(
                Configuration.GetSection("Reason for search"));

            services.Configure<List<DesignationsForNameSearchSelection>>(Configuration.GetSection("Designation"));

            // services.AddTransient<INameSearchRepository, NameSearchRepository>();

            services.AddTransient<IValueService, ValueService>();

            services.AddTransient<INameSearchService, NameSearchService>();

            services.AddTransient<IPaymentsService, PaymentsService>();

            services.AddSingleton<INameSearchMutualExclusionService, NameSearchMutualExclusionService>();

            services.AddTransient<IPrivateEntityService, PrivateEntityService>();

            services.AddScoped<IPayNowService, PayNowService>();
            
            services.AddScoped<IOutputsService, OutputsService>();

            services.AddHostedService<CheckPaymentStatusService>();
        }

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
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1"); });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            // DataConnection.DefaultSettings = new EachDbSetup();
            // DataConnection.DefaultSettings = new PoleDBSetup();
            // DataConnection.DefaultSettings = new ShwaDBSetup();
        }
    }
}