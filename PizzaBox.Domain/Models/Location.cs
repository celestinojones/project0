using System.Collections.Generic;
using PizzaBox.Data.Entities;

namespace PizzaBox.Domain.Models
{
    public class Location
    {
        private pizzaboxdbContext _db = new pizzaboxdbContext();
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }

        public Location(string a1, string a2, string city, string state, int zip)
        {
            Address1 = a1;
            Address2 = a2;
            City = city;
            State = state;
            ZipCode = zip;

            var locToDb = new Data.Entities.Location
            {
                Address1 = a1,
                Address2 = a2,
                City = city,
                State = state,
                ZipCode = zip
            };

            _db.Location.Add(locToDb);
            _db.SaveChanges();
        }
    }
}