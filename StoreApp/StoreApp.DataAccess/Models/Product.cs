using System;
using System.Collections.Generic;

namespace StoreApp.DataAccess.Models
{
    public partial class Product
    {
        public Product()
        {
            Inventory = new HashSet<Inventory>();
            Order = new HashSet<Order>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<Inventory> Inventory { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}
