using System.Collections.Generic;
using System.Linq;
using PizzaBox.Data.Entities;
using PizzaBox.Domain.Abstracts;

namespace PizzaBox.Domain.Models
{
    public class Store
    {
        pizzaboxdbContext _db = new pizzaboxdbContext();
        public int StoreId { get; set; }
        public Location StoreLocation { get; set; }
        public List<Order> Orders { get;set; }
        public List<AIngredient> Inventory { get; set; }

        public Store(List<AIngredient> inv, Location loc)
        {
            Orders = new List<Order>();
            StoreLocation = loc;
            Inventory = inv;

            foreach(var item in inv)
            {
                item.Store = this;
            }

            var locToDb = new Data.Entities.Location
            {
                Address1 = loc.Address1,
                Address2 = loc.Address2,
                City = loc.City,
                State = loc.State,
                ZipCode = loc.ZipCode
            };

            // _db.Location.Add(locToDb);
            // _db.SaveChanges();

            // var storeToDb = new Data.Entities.Store
            // {
            //     StoreNavigation = _db.Location.FirstOrDefault(l => l.Address1 == loc.Address1)
            // };

            // _db.Store.Add(storeToDb);
            // _db.SaveChanges();
        }

        public void ViewInventory()
        {
            System.Console.WriteLine($"Inventory at store ({StoreLocation.Address1})");
            foreach(var item in Inventory)
            {
                System.Console.WriteLine($"#{Inventory.IndexOf(item)+1}. {item.Name} - {item.Quantity} units");   
            }
        }

        public void ViewSales()
        {
            decimal totalSales = 0M;

            System.Console.WriteLine($"Total sales of {StoreLocation.Address1}");
            foreach(var order in Orders)
            {
                totalSales += order.Cost;
                System.Console.WriteLine($"{order.Customer.LastName} {order.Customer.EmailAddress}) - {order.Pizzas.Count} units (${order.Cost} total)");
            }
            System.Console.WriteLine($"Total sales from this location: ${totalSales}");
        }

        public void ViewUsers()
        {
            System.Console.WriteLine($"Users who have ordered from location {StoreLocation.Address1}");
            foreach(var order in Orders)
            {
                System.Console.WriteLine($"{order.Customer.FirstName} {order.Customer.LastName} - ({order.Customer.UserLocation.Address1}, {order.Customer.UserLocation.City}, {order.Customer.UserLocation.State} {order.Customer.UserLocation.ZipCode})");
            }
        }

        public void ViewCurrentOrders()
        {
            System.Console.WriteLine($"Current in-progress orders at location {StoreLocation.Address1}");
            foreach(var order in Orders)
            {
                if(!order.IsCompleted)
                {
                    System.Console.WriteLine($"Order {Orders.IndexOf(order)+1} (not completed)");

                    foreach(var pizza in order.Pizzas)
                    {
                        System.Console.WriteLine($"Pizza {order.Pizzas.IndexOf(pizza)+1}");
                        foreach(var component in pizza)
                        {
                            System.Console.WriteLine($"{component.Name} ");
                        }
                    }
                }
            }
        }
    }
}