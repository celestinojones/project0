using System.Collections.Generic;
using System.Linq;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Models;
using PizzaBox.Domain.Recipes;
using Xunit;

namespace PizzaBox.Testing
{
    public class TestToppings
    {
        NewYork TestPizzaFactory = new NewYork();
        Store TestStore = new Store(new List<AIngredient>(){ new Crust("New York Style", 0M, 100), new Sauce("Tomato", 0M, 100), new Topping("Pepperoni", 0M, 200), new Topping("Mozzarella cheese", 0M, 200), new Topping("Mushroom", 0M, 200), new Topping("Canadian Bacon", .59M, 200), new Topping("Olive", 0M, 500), new Topping("Green Pepper", 0M, 200) }, new Location("1234 UTA Blvd", null, "Arlington", "Texas", 76016));
        Size TestSize = new Size("Medium", 7.99M);
        List<AIngredient> ComponentList;
        decimal PizzaCost;

        [Fact]
        public void Test_Topping_Limit()
        {

            TestPizzaFactory.Make(TestStore, TestSize, new List<Topping>(){
                TestStore.Inventory.OfType<Topping>().FirstOrDefault(t => t.Name == "Mushroom"),
                TestStore.Inventory.OfType<Topping>().FirstOrDefault(t => t.Name == "Green Pepper"),
                TestStore.Inventory.OfType<Topping>().FirstOrDefault(t => t.Name == "Olive"),
                TestStore.Inventory.OfType<Topping>().FirstOrDefault(t => t.Name == "Canadian Bacon"),
            }, out ComponentList, out PizzaCost);
            Assert.True(ComponentList.Count == 5);
        }

        [Fact]
        public void Test_Minimum_Topping()
        {
            TestPizzaFactory.Make(TestStore, TestSize, new List<Topping>(), out ComponentList, out PizzaCost);

            Assert.True(ComponentList.OfType<Topping>().ToList().Count == 2);
        }
    }
}