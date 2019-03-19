using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductApi.Data;
using SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApi.Infrastructure {
	public class MessageListener {
		private string connectionString;
		private ProductRepository customerRepository;

		public MessageListener(IServiceProvider provider) {
			using (var scope = provider.CreateScope()) {
				var services = scope.ServiceProvider;
				this.customerRepository = services.GetService<ProductRepository>();

				var config = services.GetService<IConfiguration>();
				this.connectionString = config.GetConnectionString("Rabbit");
			}
		}

		private ProductInStockResponse ProductInStock(ProductInStockRequest request) {
			var response = new ProductInStockResponse();

			return response;
		}
	}
}
