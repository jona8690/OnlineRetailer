using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.Extensions.Configuration;

namespace OrderApi {
	public class MessageHandler : IDisposable {
		public IBus bus { get; private set; }

		public MessageHandler(IConfiguration config) {
			var rabbitConfig = config.GetSection("RabbitConnection");
			var connectionString = String.Format("host={0};virtualHost={1};username={2};password={3}", rabbitConfig["Host"], rabbitConfig["VirtualHost"], rabbitConfig["Username"], rabbitConfig["Password"]);
			bus = RabbitHutch.CreateBus(connectionString);
		}

		public void Dispose() {
			bus.Dispose();
		}
	}
}
