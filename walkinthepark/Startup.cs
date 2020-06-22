using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using walkinthepark.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using walkinthepark.Services;
using walkinthepark.Models;
using System.IO;

namespace walkinthepark
{
    // Configure everything after console app starts up
    public class Startup
    {
        // User Secret Keys
        private string _parkApiKey = null;
        private string _openWeatherApiKey = null;
        private string _hikingAppApiKey = null;
        private string _googleMapsJsApiKey = null;
        private string _googlePlacesApiKey = null;
        private string _googleGeolocationApiKey = null;

        // How you read anything from the Configuration engine
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Defaults below
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            Configuration.GetSection("OpenWeatherKey");
            Configuration.GetSection("NpsKey");
            Configuration.GetSection("HikingProjectKey");
            Configuration.GetSection("GooglePlacesKey");
            Configuration.GetSection("GoogleMapsJsKey");
            Configuration.GetSection("GeoKey");
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();

            // Set inactivity logout to 9 hours (Default is 14 days)
            services.ConfigureApplicationCookie(o =>
            {
                o.ExpireTimeSpan = TimeSpan.FromHours(9);
                o.SlidingExpiration = true;
            });

            // Sets token timeout period to 3 hours (default is 1 day)
            services.Configure<DataProtectionTokenProviderOptions>(o =>
            o.TokenLifespan = TimeSpan.FromHours(3));
            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration);
            services.AddRazorPages();
            services.AddHttpClient(); // Added per Tim Corey https://www.youtube.com/watch?v=cwgck1k0YKU 6/17/2020
            // Added system.net.http.json through NuGet package install
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection(); // Redirect from http to https
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
