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

        private MovieListContext _context;

        private IMoviesRepository _repository;

        public static int tempID; 


        public HomeController(ILogger<HomeController> logger, MovieListContext context, IMoviesRepository repository)
        {
            _logger = logger;
            _context = context;
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Podcast()
        {
            return View();
        }

        public IActionResult Movie()
        {
            return View(_context.Movies);
        }

        [HttpGet]
        public IActionResult AddMovie()
        {
            return View();
        }

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
                return View("Movie", _context.Movies);
            }
        }

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

        public IActionResult UpdateMovies(MoviesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var movie = _context.Movies.Single(x => x.MovieID == tempID);
                //Can't use shorter method to do in one line, because ID is not being changed
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
                return View("Movie", _context.Movies);
            }
        }
        

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
