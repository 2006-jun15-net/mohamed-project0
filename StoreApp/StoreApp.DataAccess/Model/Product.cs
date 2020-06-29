using System;
using System.Collections.Generic;

namespace StoreApp.DataAccess.Model
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }
}
