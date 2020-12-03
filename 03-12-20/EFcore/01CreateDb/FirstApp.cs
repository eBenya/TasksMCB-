using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/**
 * Create and write/read to/from dbase
 **/

namespace ConsoleTEST
{
    class Program
    {
        static void Main()
        {
            //DbContext realize IDisposable
            using (ApplicationContext db = new ApplicationContext())
            {
                User user1 = new User { Name = "Kek", Age = 14 };
                User user2 = new User { Name = "Puc", Age = 54 };

                db.Users.Add(user1);
                db.Users.Add(user2);

                db.SaveChanges();
                Console.WriteLine($"{nameof(user1)} and {nameof(user2)} are successfully saved.");

                var users = db.Users.ToList();
                Console.WriteLine("Users list:");
                foreach (var user in users)
                {
                    Console.WriteLine($"\tid:{user.Id} - Name:{user.Name} - Age:{user.Age}");
                }
            }
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public ApplicationContext()
        {
            //Checks whether the DB exist, if not create it
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder oprionBr)
        {
            //                        Server engine                        NameDB
            oprionBr.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=firstUserAppdb;Trusted_Connection=True;");
        }
    }
}
