using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleTEST
{
    class Program
    {
        static void Main()
        {
            string ceiling = new string('_', 40);

            #region Add(Create)
            Console.WriteLine("Create opperation:");

            using (ApplicationContext db = new ApplicationContext())
            {
                User user1 = new User { Name = "Kek", Age = 17 };
                User user2 = new User { Name = "Puc", Age = 76 };

                db.Users.AddRange(user1, user2);
                db.SaveChanges();
            }
            Console.WriteLine(ceiling);
            #endregion

            #region Read
            Console.WriteLine("Read opperation:");

            using (ApplicationContext db = new ApplicationContext())
            {
                var users = db.Users.ToList();

                PrintCollection(users);
            }
            Console.WriteLine(ceiling);
            #endregion

            #region Update
            Console.WriteLine("Update opperation:");

            using (ApplicationContext db = new ApplicationContext())
            {
                User user = db.Users.FirstOrDefault();
                if (user!=null)
                {
                    user.Name = "Lol";
                    user.Age += 5;

                    //Update object
                    db.Users.Update(user);
                    //db.SaveChanges();
                }

                var users = db.Users.ToList();

                PrintCollection(users);
            }
            Console.WriteLine(ceiling);
            #endregion

            #region Delete
            Console.WriteLine("Delete opperation:");

            using (ApplicationContext db = new ApplicationContext())
            {
                User user = db.Users.FirstOrDefault(u => u.Name == "Puc");
                if (user != null)
                {
                    db.Users.Remove(user);
                    db.SaveChanges();
                }

                Console.WriteLine("Delete the Puc from Db:");
                PrintCollection(db.Users.ToList());

                db.Users.Remove(db.Users.FirstOrDefault());
                db.SaveChanges();

                Console.WriteLine("Deletung the second user:");
                PrintCollection(db.Users.ToList());
            }

            Console.WriteLine(ceiling);
            #endregion

        }

        static void PrintCollection(IEnumerable<User> users)
        {
            foreach (var user in users)
            {
                Console.WriteLine($"\t{user.Id}.{user.Name} - {user.Age}");
            }
        }
    }
}
