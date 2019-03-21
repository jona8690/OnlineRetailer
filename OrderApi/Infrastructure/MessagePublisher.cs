using System;
using EasyNetQ;
using SharedModels;

namespace OrderApi.Infrastructure
{
    public class MessagePublisher : IMessagePublisher, IDisposable
    {
        private IBus bus;

        public MessagePublisher(string ConnInfo)
        {
            bus = RabbitHutch.CreateBus(ConnInfo);
        }

        public bool CustomerExists(int customerNo)
        {
            var request = new CustomerExistsRequest() {
                CustomerId = customerNo
            };

            var response = bus.Request<CustomerExistsRequest, CustomerExistsResponse>(request);

            return response.Exists;
        }

        public bool CustomerGoodStanding(int customerNo)
        {
            var request = new CustomerGoodStandingRequest() {
                CustomerId = customerNo
            };

            var response = bus.Request<CustomerGoodStandingRequest, CustomerGoodStandingResponse>(request);
            return response.GoodStanding;
        }

        public void Dispose()
        {
            bus.Dispose();
        }
    }
}
