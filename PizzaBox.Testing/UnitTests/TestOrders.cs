using System.Collections.Generic;
using PizzaBox.Domain.Models;
using PizzaBox.Domain.Abstracts;
using Xunit;
using PizzaBox.Domain.Recipes;

namespace PizzaBox.Testing
{
    public class TestOrders
    {
        List<AIngredient> ComponentList;
        decimal PizzaCost;

        [Fact]
        public void Test_Order_Size()
        {
            Order testOrder = new Order() { Pizzas = new List<List<AIngredient>>(new List<AIngredient>[100]) };
            testOrder.AddPizza(new List<AIngredient>());

            Assert.True(testOrder.Pizzas.Count == 100);
        }

        [Fact]
        public void Test_Order_Compute_Total()
        {
            Order testOrder = new Order();

            var ny = new NewYork();

            var store = new Store(new List<AIngredient>(){ new Crust("New York Style", 0M, 100), new Sauce("Tomato", 0M, 100), new Topping("Pepperoni", 0M, 200), new Topping("Mozzarella cheese", 0M, 200) }, new Location("1234 UTA Blvd", null, "Arlington", "Texas", 76016));
            var size = new Size("Medium", 7.99M);
            var testList = new List<Topping>();

            ny.Make(store, size, testList, out ComponentList, out PizzaCost);
            testOrder.AddPizza(ComponentList);

            ny.Make(store, size, testList, out ComponentList, out PizzaCost);
            testOrder.AddPizza(ComponentList);
            
            ny.Make(store, size, testList, out ComponentList, out PizzaCost);
            testOrder.AddPizza(ComponentList);
            
            Assert.True(testOrder.ComputeTotal() == 23.97M);
        }

        [Fact]
        public void Test_Order_Cost_Limit()
        {
            Order testOrder = new Order();
            testOrder.AddPizza(new List<AIngredient>(){ new Topping("Caviar", 9999M, 1) });

            Assert.False(testOrder.ComputeTotal() > 5000M);
        }
    }
}