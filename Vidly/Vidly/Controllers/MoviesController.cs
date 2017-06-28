using System;
using System.Web.Mvc;
using System.Data.Entity;
using System.Linq;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
  public class MoviesController : Controller
  {
    ApplicationDbContext _context;

    public MoviesController()
    {
      _context = new ApplicationDbContext();
    }

    protected override void Dispose( bool disposing )
    {
      _context.Dispose();
    }

    public ViewResult Index()
    {
      return View();
    }

    [Route( "Movies/Details/{id}" )]
    public ActionResult Details( int id )
    {
      Movie movie =
        _context
          .Movies
          .Include( m => m.Genre )
          .FirstOrDefault( m => m.Id == id );

      if( movie == null )
      {
        return HttpNotFound();
      }

      return View( movie );
    }

    public ActionResult New()
    {
      var viewModel = new MovieFormViewModel
      {
        Genres = _context.Genres.ToList()
      };

      return View( "MovieForm", viewModel );
    }

    public ActionResult Edit( int id )
    {
      Movie movie = _context.Movies.Single( m => m.Id == id );

      var viewModel = new MovieFormViewModel( movie )
      {
        Genres = _context.Genres.ToList()
      };

      return View( "MovieForm", viewModel );
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Save( Movie movie )
    {
      if( ModelState.IsValid == false )
      {
        var viewModel = new MovieFormViewModel( movie )
        {
          Genres = _context.Genres.ToList()
        };

        return View( "MovieForm", viewModel );
      }

      if( movie.Id == 0 )
      {
        movie.DateAdded = DateTime.Now;

        _context.Movies.Add( movie );
      }
      else
      {
        Movie movieInDb = _context.Movies.Single( m => m.Id == movie.Id );

        movieInDb.Name = movie.Name;
        movieInDb.ReleaseDate = movie.ReleaseDate;
        movieInDb.GenreId = movie.GenreId;
        movieInDb.NumberInStock = movie.NumberInStock;
      }

      _context.SaveChanges();

      return RedirectToAction( "Index", "Movies" );
    }
  }
}