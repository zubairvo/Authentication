using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityBasics.AuthRequirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityBasics
{
    public class Startup
    {
        
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddAuthentication("CookieAuthentication")
                .AddCookie("CookieAuthentication", config =>
                {
                    config.Cookie.Name = "AccessCookie";
                    config.LoginPath = "/Home/Authenticate";
                });
            services.AddControllersWithViews();

            services.AddAuthorization(config =>
           {
               //var defaultAuthBuilder = new AuthorizationPolicyBuilder();
               //var defaultAuthPolicy = defaultAuthBuilder
               //.RequireAuthenticatedUser()
               //.RequireClaim(ClaimTypes.DateOfBirth)
               //.Build();

               //config.DefaultPolicy = defaultAuthPolicy;


               config.AddPolicy("Claim.DoB", policyBuilder =>
               {
                   policyBuilder.AddRequirements(new CustomRequireClaim(ClaimTypes.DateOfBirth));

               });

           });

            services.AddScoped<IAuthorizationHandler, CustomRequirementClaimHandler>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();


            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
