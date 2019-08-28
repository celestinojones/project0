using System.Collections.Generic;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Models;
using System.Linq;

namespace PizzaBox.Domain.Recipes
{
    public class NewYork: APizza
    {
        public NewYork() : base() {} 

        public override void Make(Store st, Size s, List<Topping> t, out List<AIngredient> componentList, out decimal totalCost)
        {
            _components = new List<AIngredient>();

            var nyCrust = st.Inventory.OfType<Crust>().FirstOrDefault(c => c.Name == "New York Style");
            nyCrust.Quantity--;
            _components.Add(nyCrust);

            var sauce = st.Inventory.OfType<Sauce>().FirstOrDefault(c => c.Name == "Tomato");
            sauce.Quantity--;
            _components.Add(sauce);

            var pepperoni = st.Inventory.OfType<Topping>().FirstOrDefault(tp => tp.Name == "Pepperoni");
            var mozzarella = st.Inventory.OfType<Topping>().FirstOrDefault(c => c.Name == "Mozzarella cheese");
            mozzarella.Quantity--;
            pepperoni.Quantity--;

            _components.Add(mozzarella);
            _components.Add(pepperoni);

            _components.Add(s);   

            foreach(var topping in t)
            {
                if(_components.OfType<Topping>().ToList().Count < 5)
                {  
                    st.Inventory.FirstOrDefault(tp => tp == topping).Quantity--;
                    _components.Add(topping);
                }
            }
            
            componentList = Components;
            totalCost = CalculateCost();
        }
    }
}