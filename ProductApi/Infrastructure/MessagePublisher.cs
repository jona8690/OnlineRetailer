using EasyNetQ;
using SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApi.Infrastructure {
	public class MessagePublisher :IDisposable {
		private IBus bus;

		public MessagePublisher(string ConnInfo) {
			bus = RabbitHutch.CreateBus(ConnInfo);
		}

		public void Dispose() {
			bus.Dispose();
		}

		public void OrderFulfilled(OrderShared Order) {
			var message = new OrderFulfilled {
				CustomerId = Order.CustomerId,
				OrderId = Order.Id
			};

			bus.Publish(message);
		}
	}
}
