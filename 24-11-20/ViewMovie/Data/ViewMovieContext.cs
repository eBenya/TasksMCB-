using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ViewMovie.Models;

namespace ViewMovie.Data
{
    public class ViewMovieContext : DbContext
    {
        public ViewMovieContext (DbContextOptions<ViewMovieContext> options)
            : base(options)
        {
        }

        public DbSet<ViewMovie.Models.Movie> Movie { get; set; }
    }
}
