using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieList.Models
{
    // MovieList inherits from DbContext.
    public class MovieListContext :DbContext
    {
        public MovieListContext (DbContextOptions<MovieListContext> options) : base (options)
        {

        }
        //AddMovie is a db set
        public DbSet<AddMovie> Movies { get; set; }
    }

}
