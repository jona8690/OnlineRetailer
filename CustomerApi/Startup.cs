using CustomerApi.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using CustomerApi.Repositores;
using System.Threading.Tasks;
using CustomerApi.Infrastructure;

namespace CustomerApi {
	public class Startup {
		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {
			services.AddDbContext<CustomerApiContext>(opt => opt.UseInMemoryDatabase("CustomerDatabase"));

			// Register database initializer for dependency injection
			services.AddTransient<IDbInitializer, DbInitializer>();

			services.AddScoped<CustomerRepository>();

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env) {

			// Initialize the database
			using (var scope = app.ApplicationServices.CreateScope()) {
				// Initialize the database
				var services = scope.ServiceProvider;
				var dbContext = services.GetService<CustomerApiContext>();
				var dbInitializer = services.GetService<IDbInitializer>();
				dbInitializer.Initialize(dbContext);
			}

			Task.Factory.StartNew(() =>
				new MessageListener(app.ApplicationServices)
			);

			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			} else {
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseMvc();
		}
	}
}
