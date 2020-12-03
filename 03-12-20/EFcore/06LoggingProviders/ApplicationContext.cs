using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
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
        public ApplicationContext()
        {
            //Database.EnsureDeleted();   //delete Db if it exist
            Database.EnsureCreated();   //create Db if it not exist
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CRUDApp;Trusted_Connection=True;");
            //optionsBuilder.LogTo(message => System.Diagnostics.Debug.WriteLine(message));         //capture actions by output to the degug window
            //optionsBuilder.LogTo(Console.WriteLine, new[] { RelationalEventId.CommandExecuted});    //capture actions by output to the file
        }
    }
}
