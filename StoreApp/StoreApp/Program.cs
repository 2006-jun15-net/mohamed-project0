using System;
using System.Threading;
using lib = StoreApp.Library.Models;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using da = StoreApp.DataAccess.Models;
using StoreApp.DataAccess.Repositories;
using System.Linq;
using StoreApp.DataAccess.Models;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace StoreApp
{
    public class Program
    {
        
        /// <summary>
        /// Mohamed Project 0, Grocery store
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            //initalize controller repos

            CustomerController customerController = new CustomerController();
            OrderController orderController = new OrderController();
            LocationController locationController = new LocationController();
            ProductController productController = new ProductController();

            //create user from data access model to browse menus
            da.Customer customer1 = null;

            //display recurring menu until user presses 7, quit becomes false then
            bool quit = false;
            while (!quit)
            {
                //display menu returns user input after display options
                int UserChoice = DisplayMenu();
                //switch case to branch into different functionalities
                switch (UserChoice)
                {
                    //jump to login function where users can login or create new customer
                    case 1:
                        customer1 = HelperFunctions.Login(customerController);
                        break;
                    case 2:
                        //check to see if user is logged in, then place a new order
                        if (customer1 == null)
                        {
                            Console.WriteLine("Please login.");
                            break;
                        }
                        else
                        {
                            //if program gets here, than place a new order with the controler functions and user as params
                            HelperFunctions.PlaceOrder(customer1, locationController, productController, orderController);
                        }
                        break;
                    case 3:
                        //Search for a new customer by id. method called from controller repository for customer
                        //ask for user id
                        Console.Write("Enter CustomerID:");
                        string userid = Console.ReadLine();
                        int cid;
                        
                        //input validation
                        while (!int.TryParse(userid, out cid))
                        {
                            Console.Write("\nInvalid selection. Please try again: ");
                            userid = Console.ReadLine();
                        }
                        
                        //call search for customer method that displays customer details and returns customer
                        customerController.SearchCustomerByID(cid);
                        break;
                    case 4:
                        //Get order details of an order by getting order id
                        Console.WriteLine("Select Order:");
                        orderController.DisplayOrders();
                        Console.Write("Enter OrderID: ");
                        
                        //obtain order id
                        string selection = Console.ReadLine();
                        int orderID;
                        
                        //input validation
                        while (!int.TryParse(selection, out orderID))
                        {
                            Console.WriteLine("Invalid Selection. Please try again.");
                            selection = Console.ReadLine();
                        }
                        
                        //call display function from ordercontroller to display order details
                        orderController.DisplayOrderDetails(orderID);
                        break;
                    case 5:
                        //Display order history of a store location
                        //obtain the desired locationid 
                        Console.WriteLine("Select the location you want the order history for:");
                        locationController.DisplayLocations();
                        Console.Write("Enter the LocationID: ");
                        string cinput = Console.ReadLine();
                        int locationid;
                        
                        //input validation
                        while (!int.TryParse(cinput, out locationid))
                        {
                            Console.WriteLine("Invalid Selection. Please try again.");
                            cinput = Console.ReadLine();
                        }
                        
                        //if locationid is in table, then display order history in that location
                        if (locationController.repository.GetAll().Any(s => s.LocationId == locationid))
                        {
                            Console.WriteLine($"Order history for {locationController.repository.GetById(locationid).LocationName}");
                            orderController.DisplayOrderDetailsOfStore(locationid);
                        }

                        //location was not found, display not found message
                        else
                        {
                            Console.WriteLine("This Location does not exist.");
                        }
                        break;
                    case 6:
                        //Display order history for a registered customer
                        //If customer not logged in, prompt user to login
                        if (customer1 == null)
                        {
                            Console.WriteLine("Please login.");
                        }
                        //Find display order history of the customer using order controller repo
                        else
                        {
                            Console.WriteLine($"Order history for customer {0} {1}:", customer1.FirstName, customer1.LastName);
                            orderController.DisplayOrderDetailsOfCustomer(customer1.CustomerId);
                        }
                        break;
                    case 7:
                        //if user hits 7, loop breaks and program is ended
                        quit = true;
                        break;
                }
            }

        }

        /// <summary>
        /// Displays menu that is called until while loop is broken
        /// </summary>
        /// <returns></returns>
        public static int DisplayMenu()
        {
            Begin:
            Console.WriteLine("\n                       Welcome to the Online Grocercy Market Place!");
            Console.WriteLine("------------------------------------------------------------------------------------------\n");
            Console.WriteLine("1: Login or Register");
            Console.WriteLine("2: Place an order");
            Console.WriteLine("3. Search for a customer");
            Console.WriteLine("4. Search for an order");
            Console.WriteLine("5. Display order history of a store location");
            Console.WriteLine("6. Display order history for a registered customer");
            Console.WriteLine("7: Quit\n");
            Console.Write("Please Select an Option: ");
            
            //obtain user input
            string choice = Console.ReadLine();
            int UserInput;
            
            //input validation
            if (!int.TryParse(choice, out UserInput))
            {
                Console.WriteLine("\n\nInvalid Selection. Please try again.\n-----------------------------------------------------\n");
                goto Begin;
            }
            if (UserInput<1 || UserInput > 7)
            {
                Console.WriteLine("\n\nInvalid Selection. Please try again.\n-----------------------------------------------------\n");
                goto Begin;
            }
            return UserInput;
        }
    }
}
