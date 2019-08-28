using System;
using System.Collections.Generic;
using System.Linq;
using PizzaBox.Data.Entities;
using PizzaBox.Domain.Abstracts;

namespace PizzaBox.Domain.Models
{
    public class Order
    {
        private pizzaboxdbContext _db = new pizzaboxdbContext();
        public int OrderId { get; set; }
        public User Customer { get; set; }
        public Store Store { get; set; }
        public List<List<AIngredient>> Pizzas { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; private set; }
        public decimal Cost { get; private set; }

        public Order()
        {
            Pizzas = new List<List<AIngredient>>();
            CreatedAt = DateTime.Now;
        }

        public Order(Store s, User u, List<List<AIngredient>> pizzas)
        {
            Pizzas = new List<List<AIngredient>>();
            Store = s;
            Customer = u;

            s.Orders.Add(this);
            u.Orders.Add(this);

            foreach(var pizza in pizzas)
                AddPizza(pizza);

            CreatedAt = DateTime.Now;
            Cost = ComputeTotal();

            // var orderToDb = new Data.Entities.Order
            // {
            //     UserId = _db.User.FirstOrDefault(us => us.FirstName == u.FirstName).UserId,
            //     StoreId = _db.Store.FirstOrDefault(st => st.StoreNavigation.Address1 == s.StoreLocation.Address1).StoreId,
            //     IsCompleted = IsCompleted,
            //     CreatedAt = CreatedAt,
            //     Cost = Cost
            // };

            // _db.Order.Add(orderToDb);
            // _db.SaveChanges();
            
            // foreach(var pizza in pizzas)
            // {
            //     var pizzaToDb = new Pizza();

            //     _db.Pizza.Add(pizzaToDb);
            //     _db.SaveChanges();

            //     var thisPizza = _db.Pizza.Last();
            //     foreach(var component in pizza)
            //     {
            //         var newComponent = new Ingredient();
            //         newComponent.Cost = component.Cost;
            //         newComponent.Name = component.Name;
            //         newComponent.Quantity = component.Quantity;
            //         newComponent.StoreId = _db.Store.FirstOrDefault(st => st.StoreId == s.StoreId).StoreId;
            //         _db.Ingredient.Add(newComponent);

            //         _db.SaveChanges();

            //         var thisIngredient = _db.Ingredient.Last(i => i.Name == component.Name);
            //         var ingredientRelationship = new PizzaIngredient
            //         {
            //             PizzaId = thisPizza.PizzaId,
            //             IngredientId = thisIngredient.IngredientId
            //         };

            //         _db.PizzaIngredient.Add(ingredientRelationship);

            //         _db.SaveChanges();
            //     }
            // }
        }

        public void AddPizza(List<AIngredient> pizzaToAdd)
        {
            if(Pizzas.Count < 100)
                Pizzas.Add(pizzaToAdd);
        }

        public decimal ComputeTotal()
        {
            foreach(var pizza in Pizzas)
            {
                foreach(var component in pizza)
                {
                    Cost += component.Cost;
                }
            }
            
            if(Cost > 5000M)
                Cost = 5000M;
            return Cost;
        }

        public void ViewPizzas()
        {
            foreach(var pizza in Pizzas)
            {
                System.Console.WriteLine($"Pizza {Pizzas.IndexOf(pizza)+1}:");
                foreach(var component in pizza)
                {
                    System.Console.Write($"{component.Name} ");
                }
                System.Console.WriteLine("\n");
            }
        }
    }
}