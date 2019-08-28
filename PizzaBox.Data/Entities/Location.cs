using System;
using System.Collections.Generic;

namespace PizzaBox.Data.Entities
{
    public partial class Location
    {
        public Location()
        {
            User = new HashSet<User>();
        }

        public int LocationId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }

        public virtual Store Store { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}
