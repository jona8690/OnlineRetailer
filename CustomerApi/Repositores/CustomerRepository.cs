using System.Collections.Generic;
using System.Linq;
using CustomerApi.Database;
using CustomerApi.Models;

namespace CustomerApi.Repositores
{
    public class CustomerRepository
    {
        private readonly CustomerApiContext db;

        public CustomerRepository(CustomerApiContext context)
        {
            db = context;
        }

        public Customer Get(int CustomerId)
        {
            return db.Customers.FirstOrDefault(x => x.customerId == CustomerId);
        }

        public IEnumerable<Customer> GetAll()
        {
            return db.Customers.ToList();
        }

        public void Add(Customer item)
        {
            db.Customers.Add(item);
        }

        public void Update(Customer item)
        {
            db.Customers.Update(item);
        }

        public void Delete(int CustomerId)
        {
            var item = this.Get(CustomerId);
            db.Customers.Remove(item);
        }

        public bool Exists(int CustomerId)
        {
            return db.Customers.Any(x => x.customerId == CustomerId);
        }

        public bool GoodStanding(int CustomerId)
        {
            var customer = db.Customers.FirstOrDefault(x => x.customerId == CustomerId);
            if (customer != null && customer.creditStanding == creditStanding.Good)
            {
                return true;
            }

            return false;
        }

    }
}
