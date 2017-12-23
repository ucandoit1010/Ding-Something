using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DINGSOMETHING
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
            services.AddAuthentication("MyCookieAuthenticationScheme").AddCookie("MyCookieAuthenticationScheme",
                data => {
                    data.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                    data.LoginPath = "/User/Login";
                    data.LogoutPath = "/User/Logout";
                    data.Cookie.Name = CookieAuthenticationDefaults.AuthenticationScheme;
                }
            );

            // services.AddAuthentication(option => {
            //     option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //     option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //     option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            // }).AddCookie(
            //     data => {
            //         data.ExpireTimeSpan = TimeSpan.FromMinutes(5);
            //         data.LoginPath = "/User/Login";
            //         data.LogoutPath = "/User/Logout";
            //         data.Cookie.Name = CookieAuthenticationDefaults.AuthenticationScheme;
            //     }
            // );

            services.AddMvc();




            // services.AddAuthentication("Form").AddCookie(
            //     data => {
            //         data.ExpireTimeSpan = TimeSpan.FromMinutes(10);
            //         data.LoginPath = "/User/Login";
            //         data.LogoutPath = "/User/Logout";
            //     }
            // );

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
