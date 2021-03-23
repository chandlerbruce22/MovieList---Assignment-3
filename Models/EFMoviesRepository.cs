using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieList.Models
{
    public class EFMoviesRepository :IMoviesRepository
    {
        private MovieListContext _context;

        public EFMoviesRepository(MovieListContext context)
        {
            _context = context;
        }

        public IQueryable<AddMovie> Movies => _context.Movies;
    }
}
