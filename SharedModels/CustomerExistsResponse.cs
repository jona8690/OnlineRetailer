using System;
using System.Collections.Generic;
using System.Text;

namespace SharedModels {
	public class CustomerExistsResponse {
		public int CustomerId { get; set; }
		public bool Exists { get; set; }
	}
}
