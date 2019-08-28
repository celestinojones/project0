using System.Collections.Generic;
using PizzaBox.Domain.Models;

namespace PizzaBox.Domain.Abstracts
{
    public abstract class APizza
    {
        protected List<AIngredient> _components;
        public List<AIngredient> Components 
        { 
            get
            {
                return _components;
            }
        }
        protected decimal _cost;
        public decimal Cost 
        { 
            get
            {
                return _cost;
            }
        }
        
        protected decimal CalculateCost()
        {
            foreach(var component in _components)
            {
                _cost += component.Cost;
            }

            return Cost;
        }

        public abstract void Make(Store st, Size s, List<Topping> t, out List<AIngredient> componentList, out decimal totalCost);

        protected APizza()
        {
            _components = new List<AIngredient>();
        }
    }
}