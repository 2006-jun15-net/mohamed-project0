using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StoreApp.DataAccess.Models;
using StoreApp.Library.Interfaces;
using StoreApp.Library.Models;

namespace StoreApp.DataAccess.Repositories
{
    /// <summary>
    /// Repository for orders in the database with the required functionality
    /// </summary>
    public class OrderController
    {
        public readonly IProject0Repo<OrderHistory> repository = null;

        public OrderController()
        {
            repository = new GenericRepository<OrderHistory>();
        }
        public OrderController(IProject0Repo<OrderHistory> newRepo)
        {
            this.repository = newRepo;
        }

        /// <summary>
        /// Prints content of the orders table
        /// </summary>
        public void DisplayOrders()
        {
            Console.WriteLine("Orders in Database:\n");
            foreach (var item in repository.GetAll().ToList())
            {
                Console.WriteLine($"OrderID: {item.OrderId} Total Cost: ${item.TotalCost}\n" + $"CustomerID: {item.CustomerId} Date-Time: {item.Date} {item.Time} LocationID: {item.LocationId}\n");
                Console.WriteLine("------------------------------------------------------------------------------------------------------------");
            }
        }
        /// <summary>
        /// Display Details of an order
        /// </summary>
        /// <param name="orderId">The id of the order</param>
        public void DisplayOrderDetails(int orderId)
        {
            if (repository.GetAll().Any(o => o.OrderId == orderId))
            {
                var context = new NewDataBaseContext(GenericRepository<OrderHistory>.Options);
                var order = context.OrderHistory
                    .Include(o => o.Order)
                        .ThenInclude(or => or.Product)
                    .First(o => o.OrderId == orderId);

                Console.WriteLine($"OrderID: {order.OrderId} Total Cost: ${order.TotalCost}\n" + 
                    $"CustomerID: {order.CustomerId} Date-Time: {order.Date} {order.Time} LocationID: {order.LocationId}\n");

                foreach (var a in order.Order)
                {
                    Console.WriteLine($"Product: {a.Product.ProductName}\nPrice: ${a.Product.Price}\nQty: {a.Amount}\n");
                }
                context.Dispose();
            }
            else
            {
                Console.WriteLine($"\nSorry, Order not found.\n");
            }

        }

        /// <summary>
        /// Print all orderhistory of a Location
        /// </summary>
        /// <param name="locationid">Location ID</param>
        public void DisplayOrderDetailsOfStore(int locationid)
        {
            if (repository.GetAll().Any(o => o.LocationId == locationid))
            {
                List<OrderHistory> orders = repository.GetAll().Where(o => o.LocationId == locationid).ToList();
                foreach (var order in orders)
                {
                    DisplayOrderDetails(order.OrderId);
                }
            }
            else
            {
                Console.WriteLine($"\nNo orders found at this location.\n");
            }

        }

        /// <summary>
        /// Displays all orders of a customr
        /// </summary>
        /// <param name="customerid">The customer's id</param>
        public void DisplayOrderDetailsOfCustomer(int customerid)
        {
            if (repository.GetAll().Any(o => o.CustomerId == customerid))
            {
                List<OrderHistory> orders = repository.GetAll().Where(o => o.CustomerId == customerid).ToList();
                foreach (var order in orders)
                {
                    DisplayOrderDetails(order.OrderId);
                }
            }
            else
            {
                Console.WriteLine($"No orders found for this customer.");
            }
        }

    }
}
