using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StoreApp.DataAccess.Models;
using StoreApp.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreApp.DataAccess.Repositories
{
    /// <summary>
    /// Repository for Products table in the database with the required functionality
    /// </summary>
    public class ProductController
    {
        public readonly IProject0Repo<Models.Product> repository = null;

        public ProductController()
        {
            repository = new GenericRepository<Models.Product>();
        }
        public ProductController(IProject0Repo<Models.Product> newRepo)
        {
            repository = newRepo;
        }

        /// <summary>
        /// Displays all products in database
        /// </summary>
        public void DisplayProducts()
        {   //loops through products in data base with name and price
            Console.WriteLine("All Products:\n");
            foreach (var item in repository.GetAll().ToList())
            {
                Console.WriteLine($"Product Name: {item.ProductName} ProductID: {item.ProductId}\n");
            }
        }
    }
}
