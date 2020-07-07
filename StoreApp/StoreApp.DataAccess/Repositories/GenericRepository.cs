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

        //database context that each repository will use to access database
        private NewDataBaseContext _context = null;
       
        private DbSet<T> table = null;
        /// <summary>
        /// generic repo constructor, used for intializing the repositories with the context and table using options
        /// </summary>
        public GenericRepository()
        {
            this._context = new NewDataBaseContext(Options);
            table = _context.Set<T>();
        }
        /// <summary>
        /// constuctor intializing the context and table with the context generated from the constructor taking in options
        /// </summary>
        /// <param name="_context"></param>
        public GenericRepository(NewDataBaseContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }
        /// <summary>
        /// method that adds an object(record) into the table , takes in an object
        /// </summary>
        /// <param name="obj"></param>
        public void Add(T obj)
        {
            table.Add(obj);
        }
        /// <summary>
        /// method for deleting an object(record) in the table, using the id of the object thats passed in
        /// </summary>
        /// <param name="id"></param>
        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }
        /// <summary>
        /// method returning a collection of items in the table
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAll()
        {
            return table;
        }

        /// <summary>
        /// get method for retrieving a specific object(record) from a database table using the object id as a parameter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById(object id)
        {
            return table.Find(id);
        }

        /// <summary>
        /// saves changes to the database
        /// </summary>

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
