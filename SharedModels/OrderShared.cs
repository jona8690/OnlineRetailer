using System;
using System.Collections.Generic;
using System.Text;

namespace SharedModels {
	public class OrderShared {
		public int Id { get; set; }
		public DateTime? Date { get; set; }
		public Dictionary<int, int> Items { get; set; }
		public int CustomerId { get; set; }
	}
}
