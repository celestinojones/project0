using System;
using System.Collections.Generic;
using System.Linq;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Models;
using PizzaBox.Domain.Recipes;
using System.Text.RegularExpressions;


namespace PizzaBox.Client
{
    class Program
    {
        private static List<Size> _possibleSizes = new List<Size>(){
            new Size("Small", 5.99M),
            new Size("Medium", 7.99M),
            new Size("Large", 9.99M),
            new Size("X-Large", 11.99M),
       };
        static void Main(string[] args)
        {
            var p = new Program();

            var newUser = p.MakeNewUser();

            var store = p.MakeStore();

            var thisOrder = p.OrderFromStore(store, newUser);

            newUser.ViewOrderHistory();
            thisOrder.ViewPizzas();

            store.ViewCurrentOrders();

            store.ViewInventory();
        }

        public User MakeNewUser()
        {
            System.Console.WriteLine("Welcome. To get started ordering pizza, you must create an account or login.");
            System.Console.WriteLine("1. Login  2. Create account");
            var input = Console.ReadLine();
            while(string.Compare(input, "1") != 0 & string.Compare(input, "2") != 0)
            {
                System.Console.WriteLine("Choose 1 to login or 2 to create an account.");
                input = Console.ReadLine();
            }

            if(input == "1")
            {
                System.Console.WriteLine("---Login for pizza---");
                System.Console.Write("Email Address: ");
                var logEmail = Console.ReadLine();

                System.Console.Write("Password: ");
                var logPw = Console.ReadLine();
            }
            else
            {
                System.Console.WriteLine("---Register an account---");
                System.Console.Write("First name: ");

                var fn = Console.ReadLine();
                while(fn.Length < 3 || fn.Any(char.IsDigit))
                {
                    System.Console.WriteLine("Invalid name. Try again.");
                    System.Console.Write("First name: ");
                    fn = Console.ReadLine();
                }   

                System.Console.Write("Last name: ");

                var ln = Console.ReadLine();
                while(ln.Length < 3 || ln.Any(char.IsDigit))
                {
                    System.Console.WriteLine("Invalid name. Try again.");
                    System.Console.Write("Last name: ");
                    ln = Console.ReadLine();
                }  

                System.Console.Write("Email address: ");

                var email = Console.ReadLine();

                Regex emailReg = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = emailReg.Match(email);

                while(email.Length < 8 | !match.Success)
                {
                    System.Console.WriteLine("Invalid email. Try again.");
                    System.Console.Write("Email address: ");
                    
                    email = Console.ReadLine();
                    match = emailReg.Match(email);
                }

                System.Console.Write("Password: ");

                var pw = Console.ReadLine();

                while(pw.Length < 8)
                {
                    System.Console.WriteLine("Password must be more than 8 characters. Try again.");
                    System.Console.Write("Password: ");
                    
                    pw = Console.ReadLine();
                }
                
                System.Console.WriteLine("Thanks! Nearly done. What address should we deliver to?");

                System.Console.Write("Address line 1: ");
                var a1 = Console.ReadLine();
                while(a1.Length < 5)
                {
                    System.Console.WriteLine("Your address is too short! Try again.");
                    System.Console.Write("Address line 1: ");
                    a1 = Console.ReadLine();
                }

                System.Console.Write("Address line 2 (can be left blank): ");
                var a2 = Console.ReadLine();

                System.Console.Write("City: ");
                var city = Console.ReadLine();
                while(city.Length < 1 || city.Any(char.IsDigit))
                {
                    System.Console.WriteLine("Invalid city. Try again.");
                    System.Console.Write("City: ");
                    city = Console.ReadLine();
                }

                System.Console.Write("State: ");
                var st = Console.ReadLine();
                while(st.Length < 1 || st.Any(char.IsDigit))
                {
                    System.Console.WriteLine("Invalid State. Try again.");
                    System.Console.Write("State: ");
                    st = Console.ReadLine();
                }

                System.Console.Write("Zip Code: ");
                var zip = Console.ReadLine();

                bool isValid = true;

                while(zip.Length < 1 || !zip.Any(char.IsDigit) || !isValid)
                {
                    isValid = true;

                    System.Console.WriteLine("Invalid zip code. Try again.");
                    System.Console.Write("Zip Code: ");
                    zip = Console.ReadLine();

                    try
                    {  
                        Convert.ToInt32(zip);
                    }
                    catch
                    {
                        System.Console.WriteLine("Invalid zip code. Try again. (format: 12345)");
                    }
                    finally
                    {
                        isValid = false;
                    }
                }
                
                var newUser = new User(
                    fn,
                    ln,
                    email,
                    pw,
                    new Location(a1, a2, city, st, Convert.ToInt32(zip))
                );

                System.Console.WriteLine("Account created!");
                System.Console.WriteLine("-------------------");
                
                return newUser;
            }
            return new User{
                    FirstName = "Test",
                    LastName = "test",
                    EmailAddress = "test",
                    Password = "test",
                    UserLocation = new Location("1235 test", "a2", "city", "st", 12345)
                };
        }

        public Store MakeStore()
        {
            Location newStoreLocation = new Location("1234 UTA Blvd", "uta", "Arlington", "Texas", 76016);
            var inventoryList = new List<AIngredient>()
            {
                new Crust("New York Style", 0, 100),
                new Crust("Traditional Pan", .99M, 100),
                new Crust("Deep Dish Style", 1.99M, 100),
                new Sauce("Tomato", 0M, 100),
                new Sauce("White", 0.99M, 50),
                new Topping("Pepperoni", 0M, 200),
                new Topping("Mozzarella cheese", 0M, 200),
                new Topping("Mushroom", .49M, 80),
                new Topping("Sausage", .39M, 100),
                new Topping("Canadian Bacon", .49M, 100)
            };
            return new Store(inventoryList, newStoreLocation);
        }

        public Order OrderFromStore(Store store, User user)
        {
            System.Console.WriteLine($"Based on your location, you'll be ordering from the store at {store.StoreLocation.Address1}.");

            System.Console.WriteLine("What kind of pizza do you want?");
            System.Console.WriteLine("New York Style or Deep Dish Style");
            var type = System.Console.ReadLine();

            while(store.Inventory.OfType<Crust>().FirstOrDefault(t => t.Name == type) == null)
            {
                System.Console.WriteLine("That's not in the options.");
                System.Console.WriteLine("What kind of pizza do you want?");

                type = System.Console.ReadLine();
            }

            System.Console.WriteLine("What size would you like?");
            System.Console.WriteLine("Small, Medium, Large, or X-Large");
            var size = System.Console.ReadLine();

            while(_possibleSizes.FirstOrDefault(s => s.Name == size) == null)
            {
                System.Console.WriteLine("That's not a valid size.");
                System.Console.WriteLine("What size would you like (Small, Medium, Large, X-Large)?");

                size = System.Console.ReadLine();
            }

            System.Console.WriteLine("What kind of toppings do you want? (you'll only be able to have up to 5 and all pizzas come with pepperoni and mozzarella cheese)");
            System.Console.WriteLine("Mushroom, Sausage, Canadian Bacon");
            var topping = System.Console.ReadLine();

            while(store.Inventory.OfType<Topping>().FirstOrDefault(t => t.Name == topping) == null)
            {
                System.Console.WriteLine("That's not in the options.");
                System.Console.WriteLine("What kind of toppings do you want?");

                topping = System.Console.ReadLine();
            }
            
            var selectedToppings = new List<Topping>{ store.Inventory.OfType<Topping>().FirstOrDefault(t => t.Name == topping) };

            if(type == "Deep Dish Style")
            {
                var deepDish = new DeepDish();
                List<AIngredient> pi;
                decimal total; 

                deepDish.Make(store, _possibleSizes.FirstOrDefault(s => s.Name == size), selectedToppings, out pi, out total);

                System.Console.WriteLine("Order created!");
            
                return new Order(store, user, new List<List<AIngredient>>{ pi } );
            }

            var nyStyle = new NewYork();
                List<AIngredient> pizza;
                decimal cost; 

                nyStyle.Make(store, _possibleSizes.FirstOrDefault(s => s.Name == size), selectedToppings, out pizza, out cost);

                System.Console.WriteLine("Order created!");

                return new Order(store, user, new List<List<AIngredient>>{ pizza } );
        }
    }
}
