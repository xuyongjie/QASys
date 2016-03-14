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
    public class QuestionAttentionsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/QuestionAttentions
        public IQueryable<QuestionAttention> GetQuestionAttentions()
        {
            return db.QuestionAttentions;
        }

        // GET: api/QuestionAttentions/5
        [ResponseType(typeof(QuestionAttention))]
        public IHttpActionResult GetQuestionAttention(int id)
        {
            QuestionAttention questionAttention = db.QuestionAttentions.Find(id);
            if (questionAttention == null)
            {
                return NotFound();
            }

            return Ok(questionAttention);
        }

        // PUT: api/QuestionAttentions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutQuestionAttention(int id, QuestionAttention questionAttention)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != questionAttention.Id)
            {
                return BadRequest();
            }

            db.Entry(questionAttention).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionAttentionExists(id))
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

        // POST: api/QuestionAttentions
        [ResponseType(typeof(QuestionAttention))]
        public IHttpActionResult PostQuestionAttention(QuestionAttention questionAttention)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.QuestionAttentions.Add(questionAttention);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = questionAttention.Id }, questionAttention);
        }

        // DELETE: api/QuestionAttentions/5
        [ResponseType(typeof(QuestionAttention))]
        public IHttpActionResult DeleteQuestionAttention(int id)
        {
            QuestionAttention questionAttention = db.QuestionAttentions.Find(id);
            if (questionAttention == null)
            {
                return NotFound();
            }

            db.QuestionAttentions.Remove(questionAttention);
            db.SaveChanges();

            return Ok(questionAttention);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool QuestionAttentionExists(int id)
        {
            return db.QuestionAttentions.Count(e => e.Id == id) > 0;
        }
    }
}