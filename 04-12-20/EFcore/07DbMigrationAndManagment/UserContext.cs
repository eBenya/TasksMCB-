using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTEST
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        //Here we will use migration.
        public UserContext()
        {
            //Database.EnsureCreated(); <-- This is why we removed this methid from the constructor.
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TestDb;Trusted_Connection=True;");
        }
    }
}
