using System;
using System.Collections.Generic;
using System.Text;
using StoreApp.DataAccess.Models;
using StoreApp.Library.Interfaces;
using System.Linq;
using StoreApp.Library.Models;

namespace StoreApp.DataAccess.Repositories
{
    /// <summary>
    /// Repository for customers in the database with the required functionality
    /// </summary>
    public class CustomerController
    {
        /// <summary>
        /// Creates customer repo for data manipulation of table
        /// </summary>
        public readonly IProject0Repo<Models.Customer> repository = null;
        
        /// <summary>
        /// repo constructors
        /// </summary>
        public CustomerController()
        {
            repository = new GenericRepository<Models.Customer>(); 
        }
        public CustomerController(IProject0Repo<Models.Customer> newRepo)
        {
            this.repository = newRepo;
        }

        /// <summary>
        /// Display all customers from the Customer table
        /// </summary>
        public void DisplayCustomers()
        {
            Console.WriteLine("Customers in Database: ");
            foreach(var item in repository.GetAll().ToList())
            {
                Console.WriteLine($"Name: {0} {1} ID: {2}\n", item.FirstName, item.LastName, item.CustomerId);
            }
        }
        /// <summary>
        /// Takes in customerID which is searched for in the customer table and returns that customer, if no mathcing id found, return null and display error message
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Models.Customer SearchCustomerByID(int ID)
        {
            if (repository.GetAll().Any(cust => cust.CustomerId.Equals(ID)))
            {
                Models.Customer customer = repository.GetAll().First(cust => cust.CustomerId.Equals(ID));
                Console.WriteLine($"Customer: {0} {1} with id {2}",customer.FirstName,customer.LastName, customer.CustomerId);
                return customer;
            }
            else
            {
                Console.WriteLine($"No customers exist with this ID.");
                return null;
            }

        }
    }
}
