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
using QA.Repo;
using Entity.EntityDTO;

namespace QA.Controllers
{
    public class QuestionsController : ApiController
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        private readonly IQuestionRepository repo = new QuestionRepository();
        // GET: api/Questions
        public IEnumerable<QuestionDTO> GetQuestions()
        {
            return repo.GetAllQuestions();
        }

        // GET: api/Questions/5
        [ResponseType(typeof(Question))]
        public IHttpActionResult GetQuestion(int id)
        {
            QuestionDetailDTO question = repo.GetQuestionById(id);
            if (question == null)
            {
                return NotFound();
            }

            return Ok(question);
        }

        // PUT: api/Questions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutQuestion(int id, Question question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != question.Id)
            {
                return BadRequest();
            }

            int result=repo.UpdateQuestion(id, question);
            if(result==1)
            {
                    return StatusCode(HttpStatusCode.NoContent);
            }
            return BadRequest("请求的Question不存在或者未知错误");
        }

        // POST: api/Questions
        [ResponseType(typeof(Question))]
        public IHttpActionResult PostQuestion(Question question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            repo.CreateQuestion(question);

            return CreatedAtRoute("DefaultApi", new { id = question.Id }, question);
        }

        // DELETE: api/Questions/5
        [ResponseType(typeof(Question))]
        public IHttpActionResult DeleteQuestion(int id)
        {
            return Ok(repo.DeleteQuestionById(id));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}