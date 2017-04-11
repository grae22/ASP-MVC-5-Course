using System.Web.Mvc;
using System.Collections.Generic;
using Vidly.Models;

namespace Vidly.Controllers
{
  public class MoviesController : Controller
  {
    public ViewResult Index()
    {
      var movies = new List<Movie>()
      {
        new Movie() { Name = "Shrek" },
        new Movie() { Name = "Wall-e" }
      };
      
      return View( movies );
    }
  }
}