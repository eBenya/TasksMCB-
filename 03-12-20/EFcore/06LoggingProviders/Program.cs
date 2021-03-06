﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            #region Add(Create)
            sb.Append("Create opperation:\n");

            using (ApplicationContext db = new ApplicationContext())
            {
                //db.GetService<ILoggerFactory>().AddProvider(new MyLoggerProvider());

                User user1 = new User { Name = "Kek", Age = 17 };
                User user2 = new User { Name = "Puc", Age = 76 };

                db.Users.AddRange(user1, user2);
                db.SaveChanges();
            }
            #endregion

            #region Delete
            sb.Append("Delete opperation:\n");

            using (ApplicationContext db = new ApplicationContext())
            {
                User user = db.Users.FirstOrDefault(u => u.Name == "Puc");
                if (user != null)
                {
                    db.Users.Remove(user);
                    db.SaveChanges();
                }

                sb.Append("Delete the Puc from Db:\n");
                sb.Append(PrintCollection(db.Users.ToList(), ceiling));

                user = db.Users.FirstOrDefault();
                db.Users.Remove(user);
                db.SaveChanges();

                sb.Append("Delete the second user:\n");
                sb.Append(PrintCollection(db.Users.ToList(), ceiling));
            }
            #endregion


            Console.WriteLine(sb);
        }

        static string PrintCollection(IEnumerable<User> users, string separator = default)
        {
            StringBuilder sb = new StringBuilder(256);
            if (!string.IsNullOrEmpty(separator))
            {
                sb.Append($"{separator}\n");
            }

            foreach (var user in users)
            {
                sb.Append($"\t{user.Id}.{user.Name} - {user.Age}\n");
            }

            sb.Append($"{separator}\n");

            return sb.ToString();
        }
    }
}
