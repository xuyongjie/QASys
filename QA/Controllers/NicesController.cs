using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Entity;
using QA.Models;

namespace QA.Controllers
{
    public class NicesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Nices
        public IQueryable<Nice> GetNices()
        {
            return db.Nices;
        }

        // GET: api/Nices/5
        [ResponseType(typeof(Nice))]
        public IHttpActionResult GetNice(int id)
        {
            Nice nice = db.Nices.Find(id);
            if (nice == null)
            {
                return NotFound();
            }

            return Ok(nice);
        }

        // PUT: api/Nices/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNice(int id, Nice nice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nice.Id)
            {
                return BadRequest();
            }

            db.Entry(nice).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NiceExists(id))
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

        // POST: api/Nices
        [ResponseType(typeof(Nice))]
        public IHttpActionResult PostNice(Nice nice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Nices.Add(nice);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = nice.Id }, nice);
        }

        // DELETE: api/Nices/5
        [ResponseType(typeof(Nice))]
        public IHttpActionResult DeleteNice(int id)
        {
            Nice nice = db.Nices.Find(id);
            if (nice == null)
            {
                return NotFound();
            }

            db.Nices.Remove(nice);
            db.SaveChanges();

            return Ok(nice);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NiceExists(int id)
        {
            return db.Nices.Count(e => e.Id == id) > 0;
        }
    }
}