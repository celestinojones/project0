using PizzaBox.Domain.Abstracts;

namespace PizzaBox.Domain.Models
{
    public class Sauce : AIngredient
    {
        public int SauceId { get; set; }

        public Sauce(string name, decimal price, int quantityStart) : base(name, price, quantityStart) { }
    }
}