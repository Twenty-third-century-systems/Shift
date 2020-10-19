using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using DanceFlow.Hubs;
using LinqToDB.AspNet;
using LinqToDB.AspNet.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;

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
                    options.Authority = "https://localhost:5002";

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