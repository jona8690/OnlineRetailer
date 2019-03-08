using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerApi.Repositores;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
		private CustomerRepository repository;

		public CustomerController(CustomerRepository repo) {
			this.repository = repo;
		}

        // GET: api/Customer
        [HttpGet]
        public IActionResult Get()
        {
			return Ok(repository.GetAll());
        }

        // GET: api/Customer/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
			return Ok(repository.Get(id));
        }

        // POST: api/Customer
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Customer/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
