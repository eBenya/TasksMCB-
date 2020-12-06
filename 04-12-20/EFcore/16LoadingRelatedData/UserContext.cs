using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTEST
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Position> Positions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=TestDb;Trusted_Connection=True;");
            optionsBuilder.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information);
        }
    }

    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CapitalId { get; set; }
        public virtual City Capital { get; set; }

        public virtual List<Company> Companies { get; set; } = new List<Company>();
    }
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CountryId { get; set; }
        public virtual Country Country { get; set; }

        public virtual List<User> Users { get; set; } = new List<User>();
    }
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public int? PositionId { get; set; }
        public virtual Position Position { get; set; }
    }
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<User> Users { get; set; } = new List<User>();
    }
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
