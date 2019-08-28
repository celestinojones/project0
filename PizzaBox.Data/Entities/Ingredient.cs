using System;
using System.Collections.Generic;

namespace PizzaBox.Data.Entities
{
    public partial class Ingredient
    {
        public Ingredient()
        {
            PizzaIngredient = new HashSet<PizzaIngredient>();
        }

        public int IngredientId { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public int Quantity { get; set; }
        public int StoreId { get; set; }

        public virtual ICollection<PizzaIngredient> PizzaIngredient { get; set; }
    }
}
