using System.Collections.Generic;
using PizzaBox.Domain.Abstracts;

namespace PizzaBox.Domain.Models
{
    public class Crust : AIngredient
    {
       public Crust(string crustType, decimal cost, int quantity) : base(crustType, cost, quantity)
       { }
    }
}