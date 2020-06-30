using System;
using System.Threading;
using StoreApp.Library.Models;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using StoreApp.DataAccess.Models;

namespace StoreApp
{
    public class Program
    {
        
        
        public static void Main(string[] args)
        {
            //add repositories
            RunUI();

        }

        public static void RunUI()
        {
            bool quit = false;
            while (!quit)
            {
                int UserChoice = DisplayMenu();
                switch(UserChoice)
                {
                    case 1:
                        break;
                    case 2:
                        AddCustomer();
                        DisplayMenu();
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                    case 7:
                        quit = true;
                        break;
                }
            }



        }

        public static int DisplayMenu()
        {
            Begin:
            Console.WriteLine("Welcome to the Online Grocercy Market Place!");
            Console.WriteLine("1: Place an order");
            Console.WriteLine("2: Register as a new customer");
            Console.WriteLine("3. Search for a customer");
            Console.WriteLine("4. Search for an order");
            Console.WriteLine("5. Display order history of a store location");
            Console.WriteLine("6. Display order history for a registered customer");
            Console.WriteLine("7: Quit");
            Console.Write("What would you like to do?: ");
            int UserInput = Int32.Parse(Console.ReadLine());
            if(UserInput<1 || UserInput > 7)
            {
                Console.WriteLine("\nInvalid Selection. Please try again.\n-----------------------------------------------------\n");
                goto Begin;
            }
            return UserInput;
        }
        public static void AddCustomer()
        {
            // for adding, you also don't need to worry about foreign key values.
            // you can add/change relationships between objects via the navigation

            Console.WriteLine("Enter a new customer first name: ");
            var Firstname = Console.ReadLine();
            Console.WriteLine("Enter a new customer last name: ");
            var Lastname = Console.ReadLine();


            // after savechanges, any new or updated stuff implicit in what i coded before
            //   is filled in by EF for you on those tracked objects.
        }

        /*
         * functionality
            place orders to store locations for customers
            add a new customer
            search customers by name
            display details of an order
            display all order history of a store location
            display all order history of a customer
            input validation
            exception handling
            persistent data; no prices, customers, order history, etc. hardcoded in C#
            (optional: order history can be sorted by earliest, latest, cheapest, most expensive)
            (optional: get a suggested order for a customer based on his order history)
            (optional: display some statistics based on order history)
            (optional: asynchronous network & file I/O)
            (optional: logging of exceptions, EF SQL commands, and other events to a file) [new]
            (optional: deserialize data from disk) [now optional]
            (optional: serialize data to disk) [now optional]
            design
            use EF Core (database-first approach) [new]
            use an Azure SQL DB in third normal form [new]
            include a .sql script that can create the database schema and initial data [new]
            don't use public fields
            define and use at least one interface
            core / domain / business logic
            class library
            contains all business logic
            contains domain classes (customer, order, store, product, etc.)
            documentation with <summary> XML comments on all public types and members (optional: <params> and <return>)
            (recommended: has no dependency on UI, data access, or any input/output considerations)
            customer
            has first name, last name, etc.
            (optional: has a default store location to order from)
            order
            has a store location
            has a customer
            has an order time (when the order was placed)
            can contain multiple kinds of product in the same order
            rejects orders with unreasonably high product quantities
            (optional: some additional business rules, like special deals)
            location
            has an inventory
            inventory decreases when orders are accepted
            rejects orders that cannot be fulfilled with remaining inventory
            (optional: for at least one product, more than one inventory item decrements when ordering that product)
            product (etc.)
            user interface
            interactive console application
            has only display- and input-related code
            low-priority component, will be replaced when we move to project 1
            data access (recommended) [new]
            class library
            recommended separate project for data access code using EF Core
            contains data access logic but no business logic
            use repository pattern for separation of concerns
            test
            at least 10 test methods
            focus on unit testing business logic; testing the console app is very low priority
                    */
    }
}
