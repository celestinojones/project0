using PizzaBox.Data.Entities;
using PizzaBox.Domain.Models;

namespace PizzaBox.Domain.Abstracts
{
    public abstract class AIngredient
    {
        pizzaboxdbContext _db = new pizzaboxdbContext();
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public int Quantity { get; set; }
        public Domain.Models.Store Store { get; set; }

        public AIngredient(string name, decimal cost)
        {
            Name = name;
            Cost = cost;
            Quantity = 100;

            var ingToDb = new Data.Entities.Ingredient
            {
                Cost = cost,
                Name = name,
                Quantity = 100
            };

            _db.Ingredient.Add(ingToDb);
            _db.SaveChanges();
        }

        public AIngredient(string name, decimal cost, int quantityStart)
        {
            Name = name;
            Quantity = quantityStart;
            Cost = cost;

            var ingToDb = new Data.Entities.Ingredient
            {
                Cost = cost,
                Name = name,
                Quantity = quantityStart
            };

            _db.Ingredient.Add(ingToDb);
            _db.SaveChanges();
        }
    }
}