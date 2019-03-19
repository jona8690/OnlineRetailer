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
			} else return Ok("TEST");

			// If the order could not be created, "return no content".
			return NoContent();
        }

        [HttpPost]
        public IActionResult AcceptOrder([FromBody] Order order)
        {

            var customer = customerRepository.Get(order.CustomerId);
            var customerGoodCredit = customer.creditStanding == creditStanding.Good;
            if (!customerGoodCredit)
            {
                return BadRequest("Customer does not have a good enough credit standing for this purchase");
            }
            else return Ok("Order Accepted");

            // If the order could not be accepted, "return no content".
            return NoContent();
        }

    }
}
