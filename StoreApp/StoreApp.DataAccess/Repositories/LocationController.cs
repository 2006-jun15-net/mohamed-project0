using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StoreApp.DataAccess.Models;
using StoreApp.Library.Interfaces;
using StoreApp.Library.Models;

namespace StoreApp.DataAccess.Repositories
{
    /// <summary>
    /// Repository for Stores(Location) table in the database with the required functionality
    /// </summary>
    public class LocationController
    {
        public readonly IProject0Repo<Models.Location> repository = null;

        public LocationController()
        {
            repository = new GenericRepository<Models.Location>();
        }

        public LocationController(IProject0Repo<Models.Location> newRepo)
        {
            repository = newRepo;
        }

        /// <summary>
        /// Display all locations
        /// </summary>
        public void DisplayLocations()
        {
            Console.WriteLine("List of Stores:\n");
            foreach (var item in repository.GetAll().ToList())
            {
                Console.WriteLine($"Location: {item.LocationName} ID: {item.LocationId}\n");
            }
        }
    }
}
