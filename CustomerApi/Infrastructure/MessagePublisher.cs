using EasyNetQ;
using SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerApi.Infrastructure {
	public class MessagePublisher : IDisposable {
		private IBus bus;

		public MessagePublisher(string ConnInfo) {
			bus = RabbitHutch.CreateBus(ConnInfo);
		}

		public void Dispose() {
			bus.Dispose();
		}

		internal void SendEmail(EmailMessage msg) {
			bus.Send("Email", msg);
		}
	}
}
