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
      var movies =
        _context
          .Movies
          .Include( m => m.Genre )
          .ToList();
      
      return View( movies );
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
  }
}