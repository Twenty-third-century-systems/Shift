using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using DanceFlow.Hubs;
using Drinkers.InternalClients.NameSearch;
using Drinkers.InternalClients.Task;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Polly;

namespace DanceFlow {
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
                    options.DefaultScheme = "ExamCookie";
                    options.DefaultChallengeScheme = "ExamOidc";
                })
                .AddCookie("ExamCookie")
                .AddOpenIdConnect("ExamOidc", options =>
                {
                    options.Authority = Configuration["Authority"];

                    options.ClientId = "72FC2454-1401-42A0-B0DD-FBE0B7DBD482";
                    options.ClientSecret = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0";
                    options.ResponseType = "code";

                    options.Scope.Add("openid");
                    options.Scope.Add("profile");
                    options.Scope.Add("scope1");
                    options.Scope.Add("offline_access");

                    options.ResponseMode = "form_post";

                    options.SaveTokens = true;
                    options.UsePkce = true;

                    options.TokenValidationParameters.RoleClaimType = "role";
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("IsExaminer", p => 
                    p.RequireRole("Examiner"));
                options.AddPolicy("IsRegistrar", p => 
                    p.RequireRole("Registrar"));
                options.AddPolicy("CanSign", p => 
                    p.RequireClaim("Policy", "can sign"));
            });            

            services.AddHttpContextAccessor();
            services.AddHttpClient<ITaskApiClientService, TaskApiClientService>(
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
            
            services.AddHttpClient<INameSearchApiService, NameSearchApiService>(
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

            services.AddControllersWithViews();

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                IdentityModelEventSource.ShowPII = true;
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapHub<NameExaminationHub>("/name/ex");
                endpoints.MapHub<PvtExaminationHub>("/pvt/ex");
            });
        }
    }
}