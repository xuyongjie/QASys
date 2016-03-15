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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace QA.Controllers
{
    public class QuestionsController : ApiController
    {
        private readonly IQuestionRepository repo = new QuestionRepository();

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: api/Questions/all
        [AllowAnonymous]
        [ActionName("All")]
        public IEnumerable<QuestionDTO> GetAllQuestions()
        {
            return repo.GetAllQuestions();
        }

        //GET: api/Questions/attention
        [ActionName("Attention")]
        public IEnumerable<QuestionDTO> GetAttentionQuestions()
        {
            return repo.GetAttentionQuestions(UserManager.FindByName(User.Identity.Name).Id);
        }

        // GET: api/Questions/detail/5
        [AllowAnonymous]
        [ResponseType(typeof(QuestionDetailDTO))]
        [ActionName("Detail")]
        public IHttpActionResult GetQuestionDetail(int id)
        {
            QuestionDetailDTO question = repo.GetQuestionDetailById(id);
            if (question == null)
            {
                return NotFound();
            }

            return Ok(question);
        }

        // PUT: api/Questions/update/5
        [ResponseType(typeof(string))]
        [ActionName("Update")]
        public IHttpActionResult PutQuestion(int id, Question question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int result=repo.UpdateQuestion(UserManager.FindByName(User.Identity.Name).Id,id, question);
            if(result==1)
            {
                    return StatusCode(HttpStatusCode.NoContent);
            }
            return BadRequest("请求的Question不存在或者未知错误");
        }

        // POST: api/Questions/Create
        [ResponseType(typeof(QuestionDetailDTO))]
        [ActionName("Create")]
        public IHttpActionResult PostQuestion(Question question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            repo.CreateQuestion(question);

            return CreatedAtRoute("DefaultApi", new { id = question.Id }, question);
        }

        // DELETE: api/Questions/remove/5
        [ResponseType(typeof(int))]
        [ActionName("Remove")]
        public IHttpActionResult DeleteQuestion(int id)
        {
            return Ok(repo.DeleteQuestionById(UserManager.FindByName(User.Identity.Name).Id,id));
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