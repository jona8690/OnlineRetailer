using System.Collections.Generic;
using System.Linq;
using CustomerApi.Models;

namespace CustomerApi.Database
{
    public class DbInitializer : IDbInitializer
    {
        // This method will create and seed the database.
        public void Initialize(CustomerApiContext context)
        {
            context.Database.EnsureCreated();

            // Look for any Products
            if (context.Customers.Any())
            {
                return;   // DB has been seeded
            }

            List<Customer> customers = new List<Customer>
            {
				new Customer() { name = "Jonas", email = "fake@email.com", phone = "12345678", billingAddress = "Fakestreet 16, 1234 Home", shippingAddress = null, creditStanding = creditStanding.Ok },
				new Customer() { name = "Patrick", email = "fake2@email.com", phone = "87654321", billingAddress = "Fakestreet 85, 4321 Far", shippingAddress = null, creditStanding = creditStanding.Good },
				new Customer() { name = "Carlos", email = "fake3@email.com", phone = "57392947", billingAddress = "Realstreet 22, 6800 Varde", shippingAddress = null, creditStanding = creditStanding.Bad }
            };

            context.Customers.AddRange(customers);
            context.SaveChanges();
        }
    }
}
