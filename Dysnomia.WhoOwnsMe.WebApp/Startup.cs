using System;

using Dysnomia.Common.Stats;
using Dysnomia.WhoOwnsMe.Business.Implementations;
using Dysnomia.WhoOwnsMe.Business.Interfaces;
using Dysnomia.WhoOwnsMe.Common;
using Dysnomia.WhoOwnsMe.DataAccess.Implementations;
using Dysnomia.WhoOwnsMe.DataAccess.Interfaces;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Dysnomia.WhoOwnsMe.WebApp {
	public class Startup {
		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {
			var appSettingsSection = Configuration.GetSection("AppSettings");
			services.Configure<AppSettings>(appSettingsSection);

			// Services
			services.AddTransient<IPropertyService, PropertyService>();

			// DataAccess
			services.AddTransient<IPropertyDataAccess, PropertyDataAccess>();

			services.AddControllersWithViews();
			services.AddMemoryCache();
			services.AddSession(options => {
				// Set a short timeout for easy testing.
				options.IdleTimeout = TimeSpan.FromMinutes(60);
				// You might want to only set the application cookies over a secure connection:
				options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
				options.Cookie.SameSite = SameSiteMode.Strict;
				options.Cookie.HttpOnly = true;
				// Make the session cookie essential
				options.Cookie.IsEssential = true;
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
			if (env.IsDevelopment() || env.IsEnvironment("Testing")) {
				app.UseDeveloperExceptionPage();
			} else {
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.UseSession();

			if (env.IsEnvironment("Testing")) {
				app.Use(async (context, next) => {
					if (!context.Request.Query.ContainsKey("bot") || context.Request.Query["bot"] != "true") {
						context.Session.SetString("Ip", "?");

						var date = DateTime.Now;
						date.AddSeconds(-5);
						context.Session.SetString("Time", date.ToLongDateString() + " " + date.ToLongTimeString());
					}

					await next();
				});
			} else {
				app.Use(async (context, next) => {
					StatsRecorder.NewVisit(context);

					await next();
				});
			}

			app.UseEndpoints(endpoints => {
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
