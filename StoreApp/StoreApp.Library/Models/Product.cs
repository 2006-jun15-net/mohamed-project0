using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library.Models
{
    public class Product
    {
        public double Price { get; set; }
        public int Name { get; internal set; }
        public int ProductId { get; set; }

    }
}
