using EasyNetQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductApi.Data;
using SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

		public void Start() {
			using (var bus = RabbitHutch.CreateBus(connectionString)) {
				bus.Respond<ProductInStockRequest, ProductInStockResponse>(request => ProductInStock(request));

				lock (this) {
					Monitor.Wait(this);
				}
			}
		}

		private ProductInStockResponse ProductInStock(ProductInStockRequest request) {
			var response = new ProductInStockResponse();

			return response;
		}
	}
}
