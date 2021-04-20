using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using Drinkers.ExternalApiClients.NameSearch;
using Drinkers.ExternalApiClients.Outputs;
using Drinkers.ExternalApiClients.PrivateEntity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;

namespace Dab {
    public class Startup {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = "EOnlineCookie";
                    options.DefaultChallengeScheme = "EOnlineOidc";
                })
                .AddCookie("EOnlineCookie")
                .AddOpenIdConnect("EOnlineOidc", options =>
                {
                    options.Authority = Configuration["Authority"];

                    options.ClientId = "028BED7E-F9CA-4484-8ED7-7DE3A82F40BC";
                    options.ClientSecret = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0";
                    options.ResponseType = "code";

                    options.Scope.Add("openid");
                    options.Scope.Add("profile");
                    options.Scope.Add("scope1");
                    // options.Scope.Add("scope3");
                    options.Scope.Add("offline_access");

                    options.ResponseMode = "form_post";

                    options.SaveTokens = true;
                    options.UsePkce = true;
                });

            services.AddHttpContextAccessor();
            services.AddHttpClient<INameSearchApiClientService, NameSearchApiClientService>(
                    async (serviceProvider, client) =>
                    {
                        var accessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
                        var token = await accessor.HttpContext.GetTokenAsync("access_token");
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                        client.BaseAddress = new Uri(Configuration["Api"]);
                    })
                .AddTransientHttpErrorPolicy(policy =>
                    policy.WaitAndRetryAsync(new[]
                    {
                        TimeSpan.FromMilliseconds(200),
                        TimeSpan.FromMilliseconds(500),
                        TimeSpan.FromSeconds(1)
                    }));
            
            services.AddHttpClient<IPrivateEntityApiClientService, PrivateEntityApiClientService>(
                    async (serviceProvider, client) =>
                    {
                        var accessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
                        var token = await accessor.HttpContext.GetTokenAsync("access_token");
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                        client.BaseAddress = new Uri(Configuration["Api"]);
                    })
                .AddTransientHttpErrorPolicy(policy =>
                    policy.WaitAndRetryAsync(new[]
                    {
                        TimeSpan.FromMilliseconds(200),
                        TimeSpan.FromMilliseconds(500),
                        TimeSpan.FromSeconds(1)
                    }));
            
            services.AddHttpClient<IOutputsApiService, OutputsApiService>(
                    async (serviceProvider, client) =>
                    {
                        var accessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
                        var token = await accessor.HttpContext.GetTokenAsync("access_token");
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                        client.BaseAddress = new Uri(Configuration["Api"]);
                    })
                .AddTransientHttpErrorPolicy(policy =>
                    policy.WaitAndRetryAsync(new[]
                    {
                        TimeSpan.FromMilliseconds(200),
                        TimeSpan.FromMilliseconds(500),
                        TimeSpan.FromSeconds(1)
                    }));
            
            services.AddAutoMapper(typeof(Program));

            services.AddControllersWithViews(o => o.Filters.Add(new AuthorizeFilter()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            // app.UseNodeModules();
            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{Id?}");
            });
        }
    }
}