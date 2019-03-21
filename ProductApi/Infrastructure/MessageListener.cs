using EasyNetQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductApi.Data;
using ProductApi.Models;
using SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProductApi.Infrastructure {
	public class MessageListener {
		private string connectionString;
		private IRepository<Product> productRepository;
		private MessagePublisher messagePublisher;

		public MessageListener(IServiceProvider provider) {
			using (var scope = provider.CreateScope()) {
				var services = scope.ServiceProvider;
				this.productRepository = services.GetService<ProductRepository>();
				this.messagePublisher = services.GetService<MessagePublisher>();

				var config = services.GetService<IConfiguration>();
				this.connectionString = config.GetConnectionString("Rabbit");
			}
		}

		public void Start() {
			using (var bus = RabbitHutch.CreateBus(connectionString)) {
				bus.Respond<ProductInStockRequest, ProductInStockResponse>(request => ProductInStock(request));
				bus.Receive<OrderShared>("Orders", msg => Fulfill(msg));

				lock (this) {
					Monitor.Wait(this);
				}
			}
		}

		private ProductInStockResponse ProductInStock(ProductInStockRequest request) {
			var response = new ProductInStockResponse();

			var product = productRepository.Get(request.ProductId);
			if (product.ItemsInStock >= request.Quantity)
				response.Instock = true;
			else response.Instock = false;

			return response;
		}

		private void Fulfill(OrderShared order) {
			foreach(var item in order.Items) {
				var product = productRepository.Get(item.Key);

				product.ItemsReserved += item.Value;
			}

			messagePublisher.OrderFulfilled(order);
		}
	}
}
