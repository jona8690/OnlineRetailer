using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerApi.Models {
	public class Customer {
		public int customerId { get; set; }
		public string name { get; set; }
		public string email { get; set; }
		public string phone { get; set; }
		public string billingAddress { get; set; }
		public string shippingAddress { get; set; }
		public creditStanding creditStanding { get; set; }

	}
}
