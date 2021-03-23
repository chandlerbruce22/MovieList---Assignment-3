using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieList.Models
{
    public interface IMoviesRepository
    {
        // AddMovie Model is an Iqueryable.
        IQueryable<AddMovie> Movies { get; }
    }
}
