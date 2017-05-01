using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Movie_review.Models;

namespace Movie_review.Controllers
{
    public class moviesController : ApiController
    {
        HttpClient client = new HttpClient();
        private WebServiceEntities2 db = new WebServiceEntities2();

        // GET: api/movies
        [HttpGet]
        public IQueryable<movie> Getmovies()
        {
            return db.movies;
        }

        // GET: api/movies/5
        [ResponseType(typeof(movie))]
        public async Task<IHttpActionResult> Getmovie(string name)
        {
            movie movie = await db.movies.FindAsync(name);
            if (movie == null)
            {

                return NotFound();
            }

            return Ok(movie);
        }

        // PUT: api/movies/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putmovie(string name , movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (name != movie.name)
            {
                return BadRequest();
            }

            db.Entry(movie).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!movieExists(name))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/movies
        [ResponseType(typeof(movie))]
        public async Task<IHttpActionResult> Postmovie(movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.movies.Add(movie);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (movieExists(movie.name))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = movie.MovieId }, movie);
        }

        // DELETE: api/movies/5
        [ResponseType(typeof(movie))]
        public async Task<IHttpActionResult> Deletemovie(int id)
        {
            movie movie = await db.movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            db.movies.Remove(movie);
            await db.SaveChangesAsync();

            return Ok(movie);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool movieExists(string name)
        {
            return db.movies.Count(e => e.name == name) > 0;
        }
    }
}