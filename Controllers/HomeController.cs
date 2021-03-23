using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieList.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MovieList.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // Initialize the context, repository, and tempID for use later in the code.

        private MovieListContext _context;

        private IMoviesRepository _repository;

        public static int tempID; 


        public HomeController(ILogger<HomeController> logger, MovieListContext context, IMoviesRepository repository)
        {
            _logger = logger;
            _context = context;
            _repository = repository;
        }
        // Index View
        public IActionResult Index()
        {
            return View();
        }
        // Podcast View
        public IActionResult Podcast()
        {
            return View();
        }
        // List of movies view. Pass the context movies IQueryable list.
        public IActionResult Movie()
        {
            return View(_context.Movies);
        }
        // Get for adding a movie. Returns the form of adding a movie
        [HttpGet]
        public IActionResult AddMovie()
        {
            return View();
        }
        // Post for adding a movie. Checks to see if the form is valid with the model.
        // Adds a new movie to context. Saves changes. Redirects to the movie list view with the new context.
        [HttpPost]
        public IActionResult AddMovie(AddMovie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Movies.Add(movie);
                _context.SaveChanges();
                return View("Movie", _context.Movies);
            }
            else
            {
                // If model isn't valid, redirects to the list of movies with the current movie list
                return View("Movie", _context.Movies);
            }
        }
        // Edit button for each movie. Passes the movie id to the new view model. Makes a temp id.
        [HttpPost]
        public IActionResult EditMovies(int id)
        {
            tempID = id;
            return View("EditMovie", new MoviesViewModel
            {
                MoviesModel = _context.Movies.Single(x => x.MovieID == tempID),
                ID = tempID
            });
        }
        // Saving an edited movie. Checks to see if it's valid. 
        public IActionResult UpdateMovies(MoviesViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Create a movie variable and assign it the id of the edited movie
                var movie = _context.Movies.Single(x => x.MovieID == tempID);
                //Saves each property individually, while keeping the id the same
                _context.Entry(movie).Property(x => x.Category).CurrentValue = model.MoviesModel.Category;
                _context.Entry(movie).Property(x => x.MovieTitle).CurrentValue = model.MoviesModel.MovieTitle;
                _context.Entry(movie).Property(x => x.Year).CurrentValue = model.MoviesModel.Year;
                _context.Entry(movie).Property(x => x.Director).CurrentValue = model.MoviesModel.Director;
                _context.Entry(movie).Property(x => x.Rating).CurrentValue = model.MoviesModel.Rating;
                _context.Entry(movie).Property(x => x.Edited).CurrentValue = model.MoviesModel.Edited;
                _context.Entry(movie).Property(x => x.LentTo).CurrentValue = model.MoviesModel.LentTo;
                _context.Entry(movie).Property(x => x.Notes).CurrentValue = model.MoviesModel.Notes;
                _context.SaveChanges();
                return RedirectToAction("Movie");
            }
            else
            {
                // Return to movie list view if not valid.
                return View("Movie", _context.Movies);
            }
        }
        
        // Deleting the movie. Passes the id. Removes that id and all attributes from the context. Saves changes. Redirects to movie list.
        public IActionResult DeleteMovies(int id)
        {
            _context.Remove(_context.Movies.Single(x => x.MovieID == id));
            _context.SaveChanges();
            return RedirectToAction("Movie");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
