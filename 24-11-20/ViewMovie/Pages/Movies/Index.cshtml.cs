using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ViewMovie.Models;
using ViewMovie.Data;

namespace ViewMovie.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly ViewMovie.Data.ViewMovieContext _context;

        public IndexModel(ViewMovie.Data.ViewMovieContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; }

        public async Task OnGetAsync()
        {
            Movie = await _context.Movie.ToListAsync();
        }
    }
}
