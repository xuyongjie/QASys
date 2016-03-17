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
    [Authorize]
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
            return repo.GetAttentionQuestions(User.Identity.GetUserId());
        }

        // GET: api/Questions/detail/5
        [AllowAnonymous]
        [ResponseType(typeof(QuestionDetailDTO))]
        [ActionName("Detail")]
        [HttpGet]
        public IHttpActionResult GetQuestionDetail(int id)
        {
            string userId = null;
            if (User.Identity.IsAuthenticated)
            {
                userId =User.Identity.GetUserId();
            }
            QuestionDetailDTO question = repo.GetQuestionDetailById(userId, id);
            if (question == null)
            {
                return NotFound();
            }

            return Ok(question);
        }

        // PUT: api/Questions/update/5
        [ResponseType(typeof(string))]
        [ActionName("Update")]
        [HttpPut]
        public IHttpActionResult PutQuestion(int id, Question question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int result = repo.UpdateQuestion(User.Identity.Name, id, question);
            if (result == 1)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return BadRequest("请求的Question不存在或者未知错误");
        }

        // POST: api/Questions/Create
        [ResponseType(typeof(QuestionDetailDTO))]
        [ActionName("Create")]
        [HttpPost]
        public IHttpActionResult PostQuestion(Question question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            question.UserId = User.Identity.GetUserId();
            if (repo.CreateQuestion(question) == 1)
            {
                QuestionAttention attention = new QuestionAttention();
                attention.UserId = User.Identity.GetUserId();
                attention.QuestionId = question.Id;
                IQuestionAttentionRepository repository = new QuestionAttentionRepository();
                repository.CreateQuestionAttention(attention);

                Dictionary<string, object> values = new Dictionary<string, object>();
                values.Add("controller", "questions");
                values.Add("action", "Detail");
                values.Add("id", question.Id);
                return CreatedAtRoute("DefaultApi", values, question);
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE: api/Questions/remove/5
        [ResponseType(typeof(int))]
        [ActionName("Remove")]
        [HttpDelete]
        public IHttpActionResult DeleteQuestion(int id)
        {
            return Ok(repo.DeleteQuestionById(User.Identity.GetUserId(), id));
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