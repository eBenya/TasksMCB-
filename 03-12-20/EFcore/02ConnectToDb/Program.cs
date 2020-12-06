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

namespace ConsoleTEST
{
    class Program
    {
        static void Main()
        {
            using (firstUserAppdbContext db = new firstUserAppdbContext())
            {
                var users = db.Users.ToList();
                Console.WriteLine("Users list:");

                foreach (var user in users)
                {
                    Console.WriteLine($"\t{user.Id}.{user.Name} - {user.Age}");
                }
            }
        }
    }
}
