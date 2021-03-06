﻿using OrderApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderApi.Infrastructure {
	public interface IMessagePublisher {
		bool CustomerExists(int customerNo);

		bool ItemsInStock(int ProductId, int Quantity);

		// https://www.youtube.com/watch?v=WVqg3Oz0KbI
		void PlaceOrder(Order order);
	}
}
