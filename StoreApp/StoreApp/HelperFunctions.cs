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
    public static class HelperFunctions
    {
        public static da.Customer Login(CustomerController customerController)
        {
            //Login for for customer or register new customer
            da.Customer user = new da.Customer();
        registerMenu:
            Console.WriteLine("\n                       Welcome to the Online Grocercy Market Place!");
            Console.WriteLine("------------------------------------------------------------------------------------------\n");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register new customer");
            Console.Write("Please select an option: ");

            string choice = Console.ReadLine();
            int input;
            while (!int.TryParse(choice, out input))
            {
                Console.Write("\nInvalid selection. Please try again: ");
                choice = Console.ReadLine();
            }

            switch (input)
            {
                case 1:
                    if (customerController.repository.GetAll().FirstOrDefault() == null)
                    {
                        Console.WriteLine("No customers are registered. Please register as a new customer.");
                    }
                    else
                    {
                        customerController.DisplayCustomers();
                        Console.WriteLine("\nEnter CustomerID: ");
                        choice = Console.ReadLine();
                        int customerid;
                        while (!int.TryParse(choice, out customerid))
                        {
                            Console.WriteLine("\nInvalid Selection. Please try again: ");
                            choice = Console.ReadLine();
                        }
                        if (customerController.repository.GetAll().Any(c => c.CustomerId == customerid))
                        {
                            user = customerController.repository.GetById(customerid);
                            Console.WriteLine("\nSuccessfully logged in!\n");
                        }
                        else
                        {
                            Console.WriteLine($"This customer does not exist.");
                            user = null;
                        }

                    }
                    break;

                case 2:
                    //ask user info and use parameters to create new customer
                    Console.WriteLine("Enter Your First Name:");
                    string first = Console.ReadLine();
                    Console.WriteLine("Enter Your Last Name: ");
                    string last = Console.ReadLine();

                    da.Customer newCustomer = new da.Customer { FirstName = first, LastName = last };
                    customerController.repository.Add(newCustomer);
                    customerController.repository.Save();
                    user = customerController.repository.GetById(customerController.repository.GetAll().First(c => c.LastName == last && c.FirstName == first).CustomerId);
                    Console.WriteLine($"\nRegistration successful! CustomerID: {0}\n", user.CustomerId);
                    break;

                default:
                    Console.WriteLine("Invalid selection. Please try again.");
                    goto registerMenu;
            }
            return user;

        }

        public static void PlaceOrder(da.Customer currentCustomer, LocationController lcontroller, ProductController pcontroller, OrderController oc)
        {
            //Ask for store location for order
            Console.WriteLine("Select the location you would like to order from: ");
            lcontroller.DisplayLocations();
            Console.WriteLine("Enter the LocationID: ");
            string userIn = Console.ReadLine();
            int locationid;
            while (!int.TryParse(userIn, out locationid))
            {
                Console.WriteLine("Invalid selection, please try again.\n");
                Console.WriteLine("Enter the LocationID: ");
                userIn = Console.ReadLine();
            }

            //if store exists in databse, display all proucts
            if (lcontroller.repository.GetAll().Any(s => s.LocationId == locationid))
            {
                var selectedLocation = lcontroller.repository.GetById(locationid);

                using var context = new NewDataBaseContext(GenericRepository<da.Location>.Options);
                var inventory = context.Inventory
                    .Include(i => i.Product)
                    .Where(i => i.LocationId == locationid)
                    .ToList();

                //dictonary mapping products to quantity ordered
                Dictionary<da.Product, int> shoppingCart = new Dictionary<da.Product, int>();
                bool refresh = true;
                while (refresh)
                {
                    Console.WriteLine("Select an item to add to cart:");
                    foreach (var item in inventory)
                    {
                        Console.WriteLine($"Product: {item.Product.ProductName} Price: ${item.Product.Price} ID: {item.Product.ProductId} Quantity: {item.Amount}\n");
                    }
                    Console.WriteLine("Enter the ProductID to add to order");
                    Console.WriteLine("Enter 9 to checkout");
                    Console.Write("Selection: ");
                    string userInput = Console.ReadLine();
                    int productID;
                    while (!int.TryParse(userInput, out productID))
                    {
                        Console.WriteLine("Invalid selection, please try again.");
                        Console.WriteLine("\nEnter the ProductID to add to cart");
                        Console.WriteLine("Enter 9 to checkout");
                        Console.Write("Selection: ");
                        userInput = Console.ReadLine();
                    }
                    //check to see if duplicate products were added
                    if (shoppingCart != null)
                    {
                        while (shoppingCart.Keys.Any(p => p.ProductId == productID))
                        {
                            Console.WriteLine("Duplicate item in cart. ");
                            //display items in cart
                            foreach (var item in inventory)
                            {
                                Console.WriteLine($"Product: {item.Product.ProductName} Price: ${item.Product.Price} ID: {item.Product.ProductId} In Stock: {item.Amount}\n");
                            }
                            Console.WriteLine("\nEnter the ProductID to add to cart");
                            Console.WriteLine("Enter 9 to checkout");
                            Console.Write("Selection: ");
                            userInput = Console.ReadLine();
                            while (!int.TryParse(userInput, out productID))
                            {
                                Console.WriteLine("Invalid selection, please try again.");
                                Console.WriteLine("\nEnter the ProductID to add to cart");
                                Console.WriteLine("Enter 9 to checkout");
                                Console.Write("Selection: ");
                                userInput = Console.ReadLine();
                            }
                        }
                    }
                    if (productID == 9)
                    {
                        refresh = false;
                    }
                    //Check to see if Location contains productID entered
                    if (inventory.Any(i => i.Product.ProductId == productID))
                    {
                        //check that product chosen is in stock 
                        var prdt = pcontroller.repository.GetById(productID);
                        Console.Write($"Enter amount of {prdt.ProductName}s to add to the cart:");
                        userInput = Console.ReadLine();
                        int amt;
                        //input validation
                        while (!int.TryParse(userInput, out amt))
                        {
                            Console.WriteLine("Invalid selection. Please try again.");
                            Console.Write($"Enter amount of {prdt.ProductName}s to add to the cart:");
                            userInput = Console.ReadLine();
                        }
                        //product is in stock
                        if (amt > 0)
                        {
                            Inventory inv = inventory.First(i => i.Product.ProductId == productID);
                            if (inv.Amount == 0)
                            {
                                Console.WriteLine($"{prdt.ProductName} is out of stock.");

                            }
                            else if (amt > inv.Amount)
                            {
                                Console.WriteLine($"Not enough {0}s in stock.", prdt.ProductName);
                            }
                            //If conditions above pass, update inventory,
                            else
                            {
                                shoppingCart.Add(prdt, amt);
                                inv.Amount -= amt;
                                context.Update(inv);
                                context.SaveChanges();
                                Console.WriteLine("Product added to cart!");
                            }

                        }
                        else
                        {
                            Console.WriteLine("Invalid selection. Please try again.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"This product is unavailable in the current Location.");
                    }

                }
                //If cart is not empty, update database
                if (shoppingCart.Count == 0)
                {
                    Console.WriteLine("No products were added to order.");
                }
                else
                {
                    //calculate total cost
                    decimal orderTotal = 0;
                    foreach (var item in shoppingCart.Keys)
                    {
                        orderTotal += (item.Price * shoppingCart[item]);
                    }

                    Console.WriteLine($"Order Total: {orderTotal}");


                    //Save the order to order history, create the order
                    OrderHistory newOrder = new OrderHistory { CustomerId = currentCustomer.CustomerId, LocationId = selectedLocation.LocationId, TotalCost = orderTotal };
                    oc.repository.Add(newOrder);
                    oc.repository.Save();

                    newOrder = oc.repository.GetAll().First(o => o.CustomerId.Equals(currentCustomer.CustomerId) && o.OrderId.Equals(newOrder.OrderId));

                    foreach (var item in shoppingCart.Keys)
                    {
                        var product = context.Product
                        .Include(p => p.Order)
                        .First(p => p.ProductId == item.ProductId);
                        product.Order.Add(new Order { OrderNavigation = newOrder, Amount = shoppingCart[item] });
                    }

                    context.SaveChanges();

                }

            }
            else
            {
                Console.WriteLine($"This location does not exist.");
            }

        }
    }
}
