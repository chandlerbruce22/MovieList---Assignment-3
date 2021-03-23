using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieList.Models
{
    public class EFMoviesRepository :IMoviesRepository
    {
        // Private context variable.
        private MovieListContext _context;

        //Constructor that initializes context
        public EFMoviesRepository(MovieListContext context)
        {
            _context = context;
        }

        //IQueryable with the addmovie model.
        public IQueryable<AddMovie> Movies => _context.Movies;
    }
}
