using EasyNetQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderApi.Data;
using OrderApi.Models;
using SharedModels;
using System;
using System.Threading;

namespace OrderApi.Infrastructure {
	public class MessageListener {
		private readonly string connectionString;
		private readonly IRepository<Order> repository;
		private readonly MessagePublisher messagePublisher;

		public MessageListener(IServiceProvider provider) {
			using (var scope = provider.CreateScope()) {
				var services = scope.ServiceProvider;
				messagePublisher = services.GetService<MessagePublisher>();

				var config = services.GetService<IConfiguration>();
				connectionString = config.GetConnectionString("Rabbit");
				repository = services.GetService<OrderRepository>();
			}
		}

		public void Start() {
			using (var bus = RabbitHutch.CreateBus(connectionString)) {

				bus.Subscribe<OrderFulfilled>("OrderAPI", input => ShipOrder(input));

				lock (this) {
					Monitor.Wait(this);
				}
			}
		}

		private void ShipOrder(OrderFulfilled order) {
			var item = repository.Get(order.OrderId);
			item.OrderStatus = OrderStatus.shipped;
		}
	}
}
