using EasyNetQ;
using OrderApi.Models;
using SharedModels;
using System;
using System.Collections.Generic;

namespace OrderApi.Infrastructure {
	public class MessagePublisher : IMessagePublisher, IDisposable {
		private IBus bus;

		public MessagePublisher(string ConnInfo) {
			bus = RabbitHutch.CreateBus(ConnInfo);
		}

		public void Dispose() {
			bus.Dispose();
		}

		public bool CustomerExists(int customerNo) {
			var request = new CustomerExistsRequest() {
				CustomerId = customerNo
			};

			var response = bus.Request<CustomerExistsRequest, CustomerExistsResponse>(request);

			return response.Exists;
		}

		public bool ItemsInStock(int ProductId, int Quantity) {
			var request = new ProductInStockRequest() {
				ProductId = ProductId,
				Quantity = Quantity
			};

			var response = bus.Request<ProductInStockRequest, ProductInStockResponse>(request);

			return response.Instock;
		}

		public void PlaceOrder(Order order) {
			var message = new OrderShared {
				Id = order.Id,
				CustomerId = order.CustomerId,
				Date = order.Date,
				Items = new Dictionary<int, int>()
			};

			foreach (var item in order.OrderLines) {
				message.Items.Add(item.ProductId, item.Quantity);
			}

			bus.Send("Orders", message);
		}

		internal void SendEmail(EmailMessage msg) {
			bus.Send("email", msg);
		}
	}
}
