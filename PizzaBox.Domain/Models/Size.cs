using PizzaBox.Domain.Abstracts;

namespace PizzaBox.Domain.Models
{
    public class Size : AIngredient
    {
        public Size(string name, decimal cost) : base(name, cost) { }
    }
}