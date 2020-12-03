using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTEST
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        
        //Create logger factory.
        public ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddProvider(new MyLoggerProvider());
        });

        public ApplicationContext()
        {
            //Database.EnsureDeleted();   //delete Db if it exist
            Database.EnsureCreated();   //create Db if it not exist
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CRUDApp;Trusted_Connection=True;");
            optionsBuilder.UseLoggerFactory(MyLoggerFactory);
        }
    }
}
