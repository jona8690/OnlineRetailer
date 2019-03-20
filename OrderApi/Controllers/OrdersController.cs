using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OrderApi.Data;
using OrderApi.Infrastructure;
using OrderApi.Models;
using RestSharp;

namespace OrderApi.Controllers
{
    [Route("api/Orders")]
    public class OrdersController : Controller
    {
        private readonly IRepository<Order> repository;
		private IMessagePublisher messagePublisher;

        public OrdersController(IRepository<Order> repos, IMessagePublisher messagePublisher)
        {
            repository = repos;
			this.messagePublisher = messagePublisher;
        }

        // GET: api/orders
        [HttpGet]
        public IEnumerable<Order> Get()
        {
            return repository.GetAll();
        }

        // GET api/products/5
        [HttpGet("{id}", Name = "GetOrder")]
        public IActionResult Get(int id)
        {
            var item = repository.Get(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // POST api/orders
        [HttpPost]
        public IActionResult Post([FromBody]Order order)
        {
            if (order == null)
            {
                return BadRequest();
            }

			var customerExists = messagePublisher.CustomerExists(order.CustomerId);
			if (!customerExists) {
				return BadRequest("Could not find any customers with that Id");
			}

			foreach(var line in order.OrderLines) {
				var instock = messagePublisher.ItemsInStock(line.ProductId, line.Quantity);
				if(!instock) {
					return BadRequest("A Product was not in stock.");
				}
			}


			/// Running reverse if here, all negative cases make their own return.
			/// If this statement is reached, all checks should have been processed, 
			/// and the order approved.
			return NoContent();
        }

    }
}
