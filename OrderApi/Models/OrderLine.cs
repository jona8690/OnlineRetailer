﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderApi.Models {
	public class OrderLine {
		public int Id { get; set; }
		public int ProductId { get; set; }
		public int Quantity { get; set; }
	}
}
