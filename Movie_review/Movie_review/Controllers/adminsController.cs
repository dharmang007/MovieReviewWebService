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
    public class adminsController : ApiController
    {
        private WebServiceEntities db = new WebServiceEntities();

        // GET: api/admins
        public IQueryable<admin> Getadmins()
        {
            return db.admins;
        }

        // GET: api/admins/5
        [ResponseType(typeof(admin))]
        public async Task<IHttpActionResult> Getadmin(string id)
        {
            admin admin = await db.admins.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }

            return Ok(admin);
        }

        // PUT: api/admins/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putadmin(string id, admin admin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != admin.Id)
            {
                return BadRequest();
            }

            db.Entry(admin).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!adminExists(id))
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

        // POST: api/admins
        [ResponseType(typeof(admin))]
        public async Task<IHttpActionResult> Postadmin(admin admin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.admins.Add(admin);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (adminExists(admin.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = admin.Id }, admin);
        }

        // DELETE: api/admins/5
        [ResponseType(typeof(admin))]
        public async Task<IHttpActionResult> Deleteadmin(string id)
        {
            admin admin = await db.admins.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }

            db.admins.Remove(admin);
            await db.SaveChangesAsync();

            return Ok(admin);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool adminExists(string id)
        {
            return db.admins.Count(e => e.Id == id) > 0;
        }
    }
}