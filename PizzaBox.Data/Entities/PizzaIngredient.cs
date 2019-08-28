using System;
using System.Collections.Generic;

namespace PizzaBox.Data.Entities
{
    public partial class PizzaIngredient
    {
        public int PizzaIngredientId { get; set; }
        public int PizzaId { get; set; }
        public int IngredientId { get; set; }

        public virtual Ingredient Ingredient { get; set; }
        public virtual Pizza Pizza { get; set; }
    }
}
