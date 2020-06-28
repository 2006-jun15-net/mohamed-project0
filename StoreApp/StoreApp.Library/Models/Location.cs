using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Dictionary<Product,int> StoreInventory { get; set; }
        public static Product GetProduct(string product)
        {
            throw new NotImplementedException();
        }

    }
}
