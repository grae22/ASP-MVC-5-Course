using System;
using System.Linq;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
  public class NewRentalsController : ApiController
  {
    ApplicationDbContext _context;

    public NewRentalsController()
    {
      _context = new ApplicationDbContext();
    }

    // POST: /api/newRentals
    [HttpPost]
    public IHttpActionResult CreateNewRental( NewRentalDto newRental )
    {
      if( ModelState.IsValid == false )
      {
        return BadRequest();
      }

      Customer customer = _context.Customers.Single( c => c.Id == newRental.CustomerId );

      foreach( int movieId in newRental.MovieIds )
      {
        Movie movie = _context.Movies.Single( m => m.Id == movieId );

        var rental = new Rental
        {
          Customer = customer,
          Movie = movie,
          DateRented = DateTime.Now
        };

        if( movie.NumberAvailable == 0 )
        {
          return BadRequest( "Movie is not available." );
        }

        movie.NumberAvailable--;

        _context.Rentals.Add( rental );
      }

      _context.SaveChanges();

      return Ok();
    }
  }
}
