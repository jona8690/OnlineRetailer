using EasyNetQ;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApi.Data {
	public class Messenger : IDisposable {
		public IBus bus { get; set; }

		public Messenger(IConfigurationSection config) {
			this.bus = RabbitHutch.CreateBus(String.Format("host={0};virtualHost={1};username={2};password={3}", config.GetValue<string>("Hostname"), config.GetValue<string>("VirtualHost"), config.GetValue<string>("Username"), config.GetValue<string>("Password")));
		}

		public void Dispose() {
			bus.Dispose();
		}
	}
}
