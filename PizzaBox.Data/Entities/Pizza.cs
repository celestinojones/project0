using System;
using System.Collections.Generic;

namespace PizzaBox.Data.Entities
{
    public partial class Pizza
    {
        public Pizza()
        {
            PizzaIngredient = new HashSet<PizzaIngredient>();
        }

        public int PizzaId { get; set; }
        public int OrderId { get; set; }

        public virtual Order Order { get; set; }
        public virtual ICollection<PizzaIngredient> PizzaIngredient { get; set; }
    }
}
