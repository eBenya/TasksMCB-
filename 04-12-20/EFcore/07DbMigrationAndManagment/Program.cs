using System;
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

            using (UserContext db = new UserContext())
            {
                db.Users.Add(new User { Name = "Kek", Age = 45 });
                db.Users.Add(new User { Name = "Lol", Age = 12 });

                db.SaveChanges();
            }


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
