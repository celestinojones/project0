using System;
using System.Collections.Generic;

namespace PizzaBox.Data.Entities
{
    public partial class Store
    {
        public Store()
        {
            Order = new HashSet<Order>();
        }

        public int StoreId { get; set; }
        public int LocationId { get; set; }

        public virtual Location StoreNavigation { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}
