using System;
using System.Collections.Generic;

namespace StoreApp.DataAccess.Models
{
    public partial class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
