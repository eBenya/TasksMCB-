using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTEST
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        private StreamWriter logWriter = new StreamWriter("log.txt", true);
        public ApplicationContext()
        {
            //Database.EnsureDeleted();   //delete Db if it exist
            Database.EnsureCreated();   //create Db if it not exist
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CRUDApp;Trusted_Connection=True;");
            optionsBuilder.LogTo(logWriter.WriteLine);    //capture actions by output to the file
        }

        public override void Dispose()
        {
            base.Dispose();
            logWriter.Dispose();
        }

        public override async ValueTask DisposeAsync()
        {
            await base.DisposeAsync();
            await logWriter.DisposeAsync();
        }
    }
}
