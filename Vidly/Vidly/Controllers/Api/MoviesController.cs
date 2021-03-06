﻿using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Data.Entity;
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
    public IHttpActionResult GetMovies(string query)
    {
      var moviesQuery =
        _context
          .Movies
          .Include(m => m.Genre)
          .Where(m => m.NumberAvailable > 0);

      if (!string.IsNullOrWhiteSpace(query))
      {
        moviesQuery = moviesQuery.Where(m => m.Name.Contains(query));
      }

      var movieDtos =
        moviesQuery
          .ToList()
          .Select( Mapper.Map< Movie, MovieDto > );

      return Ok(movieDtos);
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
    [Authorize( Roles=RoleName.CAN_MANAGE_MOVIES )]
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
    [Authorize( Roles=RoleName.CAN_MANAGE_MOVIES )]
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
    [Authorize( Roles=RoleName.CAN_MANAGE_MOVIES )]
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
