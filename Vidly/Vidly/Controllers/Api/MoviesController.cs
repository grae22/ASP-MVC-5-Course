using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
  public class MoviesController : ApiController
  {
    ApplicationDbContext _context;

    public MoviesController()
    {
      _context = new ApplicationDbContext();
    }

    // GET: /api/movies
    public IHttpActionResult GetMovies()
    {
      return Ok(
        _context
          .Movies
          .ToList()
          .Select( Mapper.Map< Movie, MovieDto > ) );
    }

    // GET: /api/movies/1
    public IHttpActionResult GetMovie( int id )
    {
      var movieInDb = _context.Movies.FirstOrDefault( m => m.Id == id );

      if( movieInDb == null )
      {
        return NotFound();
      }

      return Ok( Mapper.Map< Movie, MovieDto >( movieInDb ) );
    }

    // POST: /api/movies
    [HttpPost]
    public IHttpActionResult CreateMovie( MovieDto movieDto )
    {
      if( ModelState.IsValid == false )
      {
        return BadRequest();
      }

      var movie = Mapper.Map< MovieDto, Movie >( movieDto );

      movie.DateAdded = DateTime.Now;

      _context.Movies.Add( movie );
      _context.SaveChanges();

      movieDto.Id = movie.Id;

      return Created(
        new Uri( Request.RequestUri + "/" + movie.Id ),
        movieDto );
    }

    // PUT: /api/movies/1
    [HttpPut]
    public IHttpActionResult UpdateMovie( int id, MovieDto movie )
    {
      if( ModelState.IsValid == false )
      {
        return BadRequest();
      }

      var movieInDb = _context.Movies.FirstOrDefault( m => m.Id == id );

      if( movieInDb == null )
      {
        return NotFound();
      }

      Mapper.Map( movie, movieInDb );

      _context.SaveChanges();

      return StatusCode( HttpStatusCode.Accepted );
    }

    // DELETE: /api/movies/1
    public IHttpActionResult DeleteMovie( int id )
    {
      var movieInDb = _context.Movies.FirstOrDefault( m => m.Id == id );

      if( movieInDb == null )
      {
        return NotFound();
      }

      _context.Movies.Remove( movieInDb );
      _context.SaveChanges();

      return StatusCode( HttpStatusCode.Accepted );
    }
  }
}
