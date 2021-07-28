using _2021_dotnet_g_04.Data;
using _2021_dotnet_g_04.Data.Repositories;
using _2021_dotnet_g_04.Filters;
using _2021_dotnet_g_04.Models.Domain;
using _2021_dotnet_g_04.Models.Domain.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace _2021_dotnet_g_04 {
	public class Startup {

		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {
			services.AddControllersWithViews();
			services.AddRazorPages();

			services.AddDbContext<ApplicationDbContext>(options => {
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
			});

			services.AddDefaultIdentity<ApplicationUser>()
				.AddEntityFrameworkStores<ApplicationDbContext>();

			services.Configure<IdentityOptions>(options => {
				options.SignIn.RequireConfirmedAccount = false;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireDigit = false;
				options.Lockout.MaxFailedAccessAttempts = 5;
			});

			services.AddScoped<IKlantRepository, KlantRepository>();
			services.AddScoped<ITicketRepository, TicketRepository>();
			services.AddScoped<IWerknemerRepository, WerknemerRepository>();
			services.AddScoped<IContractTypeRepository, ContractTypeRepository>();
			services.AddScoped<DBInitializer>();
			services.AddScoped<KlantFilter>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DBInitializer dBInitializer) {
			Console.WriteLine($"Environement: {env.EnvironmentName}");
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
				app.UseStatusCodePages();
				dBInitializer.Run();
			}
			else {
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints => {
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
				endpoints.MapRazorPages();
			});

		}
	}
}
