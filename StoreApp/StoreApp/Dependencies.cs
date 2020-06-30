using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using StoreApp.Library.Interfaces;
using StoreApp.DataAccess.Models;

namespace StoreApp
{
    class Dependencies : IDesignTimeDbContextFactory<NewDataBaseContext>, IDisposable
    {
        public NewDataBaseContext CreateDbContext(string[] args)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
