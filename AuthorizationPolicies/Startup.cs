using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;

namespace AuthorizationPolicies
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IPassportInformationRepository, PassportInformationRepository>();

            services.AddCookieAuthentication(AuthenticationConfiguration.AuthenticationScheme, configure =>
            {
                configure.LoginPath = new PathString("/Account/Login/");
                configure.AccessDeniedPath = new PathString("/Account/Forbidden/");
            });

            services.AddAuthorization(configure =>
            {
                configure.AddPolicy(
                    AuthorizationPolicies.EUCustoms,
                    policy => 
                    policy.Requirements.Add(new CustomsLaneRequirement { Lane = CustomsLane.EU }));
                configure.AddPolicy(
                    AuthorizationPolicies.NothingToDeclare,
                    policy =>
                    policy.Requirements.Add(new CustomsLaneRequirement { Lane = CustomsLane.NothingToDeclare }));
                configure.AddPolicy(
                    AuthorizationPolicies.GoodsToDeclare,
                    policy =>
                    policy.Requirements.Add(new CustomsLaneRequirement { Lane = CustomsLane.GoodsToDeclare }));

            });

            services.AddTransient<IAuthorizationHandler, CustomsAuthorizationHandler>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                     name: "default",
                     template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
