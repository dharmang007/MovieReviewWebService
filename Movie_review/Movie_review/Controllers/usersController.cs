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
    public class usersController : ApiController
    {
        private WebServiceEntities2 db = new WebServiceEntities2();

        
        // GET: api/users
        public IQueryable<user> Getusers()
        {
            return db.users;
        }
       
        // GET: api/users/5
        [ResponseType(typeof(user))]
        [Route("Login")]
        public async Task<IHttpActionResult> Getuser(string email,string pwd)
        {
            var user = await db.users.SingleOrDefaultAsync(i => i.email == email && i.pwd == pwd);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/users/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putuser(string email, user user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (email != user.email)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!userExists(email))
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

        // POST: api/users
        [ResponseType(typeof(user))]
        [Route("UserSignUp")]
        public async Task<IHttpActionResult> Postuser(user u)
        {
            user u1 = new user();
            u1.email = u.email;
            u1.pwd = u.pwd;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.users.Add(u1);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (userExists(u.email))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(u);
        }

        // DELETE: api/users/5
        [ResponseType(typeof(user))]
        public async Task<IHttpActionResult> Deleteuser(string email)
        {
            user user = await db.users.FindAsync(email);
            if (user == null)
            {
                return NotFound();
            }

            db.users.Remove(user);
            await db.SaveChangesAsync();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool userExists(string email)
        {
            return db.users.Count(e => e.email == email) > 0;
        }
    }
}