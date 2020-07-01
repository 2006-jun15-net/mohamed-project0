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
            .UseLoggerFactory(MyLoggerFactory)
            .UseSqlServer(SecretConfiguration.ConnectionString)
            .Options;

        private NewDataBaseContext _context = null;
        public DbSet<T> table = null;

        public GenericRepository()
        {
            this._context = new NewDataBaseContext();
            table = _context.Set<T>();
        }
        public GenericRepository(NewDataBaseContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }
        public void Add(T obj)
        {
            table.Add(obj);
        }

        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }

        public IEnumerable<T> GetAll()
        {
            return table;
        }

        public T GetById(object id)
        {
            return table.Find(id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
