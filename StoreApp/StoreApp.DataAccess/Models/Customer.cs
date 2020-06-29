using System;
using System.Collections.Generic;

namespace StoreApp.DataAccess.Models
{
    public partial class Customer
    {
        public Customer()
        {
            OrderHistory = new HashSet<OrderHistory>();
        }

        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<OrderHistory> OrderHistory { get; set; }
    }
}
