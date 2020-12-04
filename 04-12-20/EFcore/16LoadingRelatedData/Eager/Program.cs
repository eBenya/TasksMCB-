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
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                Position p1 = new Position { Name = "Manager" };
                Position p2 = new Position { Name = "Developer" };
                db.Positions.AddRange(p1, p2);

                City city1 = new City { Name = "City1" };
                db.Cities.Add(city1);

                Country country1 = new Country { Name = "Country", Capital = city1 };
                db.Countries.Add(country1);

                Company c1 = new Company { Name = "C1", Country = country1 };
                Company c2 = new Company { Name = "C2", Country = country1 };
                db.Companies.AddRange(c1, c2);

                User u1 = new User { Name = "Kek", Company = c1, Position = p1 };
                User u2 = new User { Name = "Puc", Company = c2, Position = p2 };
                User u3 = new User { Name = "Tuc", Company = c1, Position = p2 };
                User u4 = new User { Name = "Lol", Company = c2, Position = p1 };
                db.Users.AddRange(u1, u2, u3, u4);

                db.SaveChanges();
            }

            using (ApplicationContext db = new ApplicationContext())
            {
                var users = db.Users
                    .Include(c => c.Company)
                        .ThenInclude(c => c.Country)
                            .ThenInclude(c=>c.Capital)
                    .Include(p => p.Position)
                    .ToList();

                foreach (var user in users)
                {
                    Console.WriteLine($"{user.Name} - {user.Position.Name}");
                    Console.WriteLine($"{user.Company?.Name} - {user.Company?.Country.Name} - {user.Company?.Country.Capital.Name}");
                    Console.WriteLine(ceiling); 
                }
            }

            Console.WriteLine(sb);
        }        
    }
}
