using System;
using System.Collections.Generic;
using System.Text;

namespace SharedModels {
	public class ProductInStockRequest {
		public int ProductId { get; set; }
		public int Quantity { get; set; }
	}
}
