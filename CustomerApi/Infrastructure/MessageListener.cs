using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CustomerApi.Repositores;
using EasyNetQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedModels;

namespace CustomerApi.Infrastructure {
	public class MessageListener {

		private string connectionString;
		private CustomerRepository customerRepository;

		public MessageListener(IServiceProvider provider) {
			using (var scope = provider.CreateScope()) {
				var services = scope.ServiceProvider;
				this.customerRepository = services.GetService<CustomerRepository>();

				var config = services.GetService<IConfiguration>();
				this.connectionString = config.GetConnectionString("Rabbit");				
			}
		}

		public void Start() {
			using (var bus = RabbitHutch.CreateBus(connectionString)) {
				bus.Respond<CustomerExistsRequest, CustomerExistsResponse>(request => CustomerExists(request));

				lock (this) {
					Monitor.Wait(this);
				}
			}
		}

		private CustomerExistsResponse CustomerExists(CustomerExistsRequest request) {
			var response = new CustomerExistsResponse() {
				CustomerId = request.CustomerId
			};

			if (customerRepository.Exists(request.CustomerId)) {
				response.Exists = true;
			} else response.Exists = false;

			return response;
		}
	}
}
