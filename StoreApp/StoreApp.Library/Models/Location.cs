using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public static Dictionary<Product,int> StoreInventory { get; set; }
        public static Product GetProduct(string product)
        {
            try
            {
                foreach (KeyValuePair<Product, int> item in StoreInventory)
                {
                    if (item.Key.Name == product)
                    {
                        return item.Key;
                    }
                }
                return null;
            }
            catch
            {
                Console.WriteLine("Product not available in this store.");
                return null;
            }


        }

    }
}
