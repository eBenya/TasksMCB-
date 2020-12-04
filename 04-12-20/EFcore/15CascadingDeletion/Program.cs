using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;

namespace ConsoleTEST
{
    class Program
    {
        static void Main()
        {
            string ceiling = new string('_', 40);

            StringBuilder sb = new StringBuilder(256);

            using (ApplicationContext db = new ApplicationContext())
            {
                Company company1 = new Company { Name = "C1" };
                Company company2 = new Company { Name = "C2" };
                User user1 = new User { Name = "Kek", Company = company1 };
                User user2 = new User { Name = "Puc", Company = company1 };
                User user3 = new User { Name = "Lol", Company = company2 };

                db.Users.AddRange(user1, user2, user3);
                db.Companies.AddRange(company1, company2);
                db.SaveChanges();

                Console.WriteLine("After creating:");
                foreach (var user in db.Users.ToList())
                {
                    Console.WriteLine($"\t{user.Name} work in {user.Company?.Name}");
                }

                var comp = db.Companies.FirstOrDefault();
                db.Companies.Remove(comp);
                db.SaveChanges();

                Console.WriteLine("After deleting:");
                foreach (var user in db.Users.ToList())
                {
                    Console.WriteLine($"\t{user.Name} work in {user.Company?.Name}");
                }
            }

            Console.WriteLine(sb);
        }        
    }
}
