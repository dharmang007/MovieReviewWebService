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
    public class reviewsController : ApiController
    {
        private WebServiceEntities2 db = new WebServiceEntities2();

        // GET: api/reviews
        public IQueryable<review> Getreviews()
        {
            return db.reviews;
        }

        // GET: api/reviews/5
        [ResponseType(typeof(review))]
        public async Task<IHttpActionResult> Getreview(int id)
        {
            review review = await db.reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            return Ok(review);
        }

        // PUT: api/reviews/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putreview(int id, review review)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != review.movieid)
            {
                return BadRequest();
            }

            db.Entry(review).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!reviewExists(id))
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

        // POST: api/reviews
        [ResponseType(typeof(review))]
        public async Task<IHttpActionResult> Postreview(review review)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.reviews.Add(review);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (reviewExists(review.movieid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = review.movieid }, review);
        }

        // DELETE: api/reviews/5
        [ResponseType(typeof(review))]
        public async Task<IHttpActionResult> Deletereview(int id)
        {
            review review = await db.reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            db.reviews.Remove(review);
            await db.SaveChangesAsync();

            return Ok(review);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool reviewExists(int id)
        {
            return db.reviews.Count(e => e.movieid == id) > 0;
        }
    }
}