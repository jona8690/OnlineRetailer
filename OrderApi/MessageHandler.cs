using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderApi {
	public class MessageHandler {
		private IBus bus { get; set; }

		public MessageHandler(BusHandler busHandler) {
			this.bus = busHandler.bus;
		}
	}
}
