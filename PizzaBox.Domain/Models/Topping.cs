using PizzaBox.Domain.Abstracts;

namespace PizzaBox.Domain.Models
{
    public class Topping : AIngredient
    {   
        public Topping(string name, decimal cost, int quantityStart) : base(name, cost, quantityStart) { }
    }
}