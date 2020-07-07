using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StoreApp.DataAccess.Models;
using System.Linq;
using StoreApp.Library.Interfaces;


namespace StoreApp.DataAccess.Repositories
{
    /// <summary>
    /// Generic store repository for handling data operations, implementing the functions from IProject0Repo
    /// </summary>
    public class GenericRepository<T> : IProject0Repo<T> where T : class
    {
        public static readonly ILoggerFactory MyLoggerFactory
            = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public static readonly DbContextOptions<NewDataBaseContext> Options = new DbContextOptionsBuilder<NewDataBaseContext>()
            //.UseLoggerFactory(MyLoggerFactory)
            .UseSqlServer(SecretConfiguration.ConnectionString)
            .Options;

        private NewDataBaseContext _context = null;
        
        private DbSet<T> table = null;

        public GenericRepository()
        {
            this._context = new NewDataBaseContext(Options);
            table = _context.Set<T>();
        }
        public GenericRepository(NewDataBaseContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }
        /// <summary>
        /// Adds a new entity to the database
        /// </summary>
        /// <param name="obj"></param>
        public void Add(T obj)
        {
            table.Add(obj);
        }
        /// <summary>
        /// Delete a entity from the database
        /// </summary>
        /// <param name="id"></param>
        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }

        /// <summary>
        /// Gets all entities of type T
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAll()
        {
            return table;
        }

        /// <summary>
        /// Retrieves a specific entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById(object id)
        {
            return table.Find(id);
        }

        /// <summary>
        /// Saves all current changes made to your Entities of type T to the Database
        /// </summary>
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
