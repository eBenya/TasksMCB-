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
            StringBuilder sb;

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

                User u5 = new User { Name = "TucPuc", Company = c2, Position = p2 };
                User u6 = new User { Name = "KekLol", Company = c2, Position = p1 };
                db.Users.AddRange(u1, u2, u3, u4, u5, u6);

                db.SaveChanges();
            }

            #region Eagly loading
            using (ApplicationContext db = new ApplicationContext())
            {
                sb = new StringBuilder(256);

                var users = db.Users
                    .Include(c => c.Company)
                        .ThenInclude(c => c.Country)
                            .ThenInclude(c => c.Capital)
                    .Include(p => p.Position);      //It this moment will be generated request, but not set
                                                    //.ToList();                    //<-- only when an entity is accessed, a request is sent.

                foreach (var user in users)     //Set query
                {
                    sb.Append($"{user.Name} - {user.Position.Name}\n");
                    sb.Append($"{user.Company?.Name} - {user.Company?.Country.Name} - {user.Company?.Country.Capital.Name}\n");
                    sb.Append($"{ceiling}\n");
                }
                Console.WriteLine(sb);
            }
            #endregion

            #region Explicit loadong
            using (ApplicationContext db = new ApplicationContext())
            {
                sb = new StringBuilder(256);
                //Я хз какой подходящий запрос под эту опперацию придумать
                //Load data about developers from "C2" company.

                var company = db.Companies.FirstOrDefault(c => c.Name == "C2");
                //db.Entry(company).Collection(c => c.Users).Load();
                db.Users.Where(u => u.Company.Name == "C2" && u.Position.Name == "Developer").Load();

                //For example Reference().Load()
                db.Entry(company.Users.FirstOrDefault()).Reference(u => u.Position).Load();

                sb.Append($"In company {company.Name} worked:\n");
                foreach (var user in company.Users)
                {
                    sb.Append($"\t{user.Name} is {user.Position.Name};\n");
                }
                Console.WriteLine(sb);
            }
            #endregion

            #region Lazy loading
            using (ApplicationContext db = new ApplicationContext())
            {
                sb = new StringBuilder(256);

                var users = db.Users.ToList();
                foreach (var user in users)
                {
                    sb.Append($"{user.Name} is {user.Position?.Name} from {user.Company?.Name}\n");
                }

                Console.WriteLine(sb);
            }
            #endregion
        }
    }
}
