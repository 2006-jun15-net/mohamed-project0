﻿using System;
using System.Collections.Generic;
using System.Text;
using da = StoreApp.DataAccess.Models;
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
        public readonly IProject0Repo<da.Customer> repository = null;
        
        /// <summary>
        /// repo constructors
        /// </summary>
        /// 

        //initizalize the controller
        public CustomerController()
        {
            repository = new GenericRepository<da.Customer>(); 
        }
        public CustomerController(IProject0Repo<da.Customer> newRepo)
        {
            this.repository = newRepo;
        }

        /// <summary>
        /// Display all customers from the Customer table
        /// </summary>
        public void DisplayCustomers()
        {

            Console.WriteLine("\n------------------------------------------------------------------------------------------\n");
            Console.WriteLine("Customers in Database:\n ");
            foreach(var item in repository.GetAll().ToList())
            {
                Console.WriteLine($"Name: {item.FirstName} {item.LastName} CustomerID: {item.CustomerId}\n");
            }
        }
        /// <summary>
        /// Takes in customer name and returns first instance of that customer
        /// </summary>
        /// 


        public da.Customer SearchCustomerByName(string query)
        {
            //lower cases the query and matches the query to lowercase first names
            if (repository.GetAll().Any(cust => cust.FirstName.ToLower().Equals(query)))
            {
                da.Customer customer = repository.GetAll().First(cust => cust.FirstName.ToLower().Equals(query));
                Console.WriteLine($"\nCustomer: {customer.FirstName} {customer.LastName} with CustomerId: {customer.CustomerId}\n" );
                return customer;
            }
            //if cant find a customer, than display error message...need to implement try catches;
            else
            {
                Console.WriteLine($"\nNo customers exist with this name.\n");
                return null;
            }

        }
    }
}
