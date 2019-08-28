using System.Collections.Generic;
using System.Linq;
using PizzaBox.Data.Entities;

namespace PizzaBox.Domain.Models
{
    public class User
    {
        private pizzaboxdbContext _db = new pizzaboxdbContext();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public Location UserLocation { get; set; }
        public List<Order> Orders { get; set; }

        public User()
        {
            Orders = new List<Order>();
        }
        
        public User(string fn, string ln, string email, string pw, Location loc)
        {
            Orders = new List<Order>();

            var locToDb = new Data.Entities.Location
            {
                Address1 = loc.Address1,
                Address2 = loc.Address2,
                City = loc.City,
                State = loc.State,
                ZipCode = loc.ZipCode
            };

            _db.Location.Add(locToDb);
            _db.SaveChanges();

            var usertoDb = new Data.Entities.User
            {
                FirstName = fn,
                LastName = ln,
                EmailAddress = email,
                Password = pw,
                Location = _db.Location.FirstOrDefault(l => l.Address1 == loc.Address1)
            };

            _db.User.Add(usertoDb);

            _db.SaveChanges();
        }

        public void ViewOrderHistory()
        {
            System.Console.WriteLine($"{FirstName}'s Order history:");
            foreach(var order in Orders)
            {
                System.Console.WriteLine($"Order {Orders.IndexOf(order)+1}");
                foreach(var pizza in order.Pizzas)
                {
                    decimal cost = 0;
                    foreach(var topping in pizza)
                    {
                        cost += topping.Cost;
                    }
                    System.Console.WriteLine($"Pizza {order.Pizzas.IndexOf(pizza)+1} - {pizza.OfType<Size>().FirstOrDefault().Name} {pizza.OfType<Crust>().FirstOrDefault().Name} (${cost})");
                }
                if(order.Cost == 0)
                    order.ComputeTotal();
                    
                System.Console.WriteLine($"Order #{Orders.IndexOf(order)+1} total:  ${order.Cost}");
            }
        }
    }
}