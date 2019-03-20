using EasyNetQ;
using SharedModels;
using System;

namespace OrderApi.Infrastructure {
	public class MessagePublisher : IMessagePublisher, IDisposable {
		private IBus bus;

		public MessagePublisher(string ConnInfo) {
			bus = RabbitHutch.CreateBus(ConnInfo);
		}

		public bool CustomerExists(int customerNo) {
			var request = new CustomerExistsRequest() {
				CustomerId = customerNo
			};

			var response = bus.Request<CustomerExistsRequest, CustomerExistsResponse>(request);

			return response.Exists;
		}

		public void Dispose() {
			bus.Dispose();
		}

		public bool ItemsInStock(int ProductId, int Quantity) {
			var request = new ProductInStockRequest() {
				ProductId = ProductId,
				Quantity = Quantity
			};

			var response = bus.Request<ProductInStockRequest, ProductInStockResponse>(request);

			return response.Instock;
		}
	}
}
