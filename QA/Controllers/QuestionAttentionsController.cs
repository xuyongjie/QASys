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
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace QA.Controllers
{
    [Authorize]
    public class QuestionAttentionsController : ApiController
    {
        private readonly IQuestionAttentionRepository repo = new QuestionAttentionRepository();

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

        // GET: api/QuestionAttentions/detail/5
        [ResponseType(typeof(QuestionAttention))]
        [ActionName("Detail")]
        [HttpGet]
        public IHttpActionResult GetNice(int id)
        {
            return Ok(repo.GetQuestionAttention(id));
        }

        // POST: api/QuestionAttentions/create
        [ResponseType(typeof(QuestionAttention))]
        [ActionName("Create")]
        [HttpPost]
        public IHttpActionResult PostQuestionAttention(QuestionAttention questionAttention)
        {
            if (repo.CreateQuestionAttention(questionAttention) == 1)
            {
                Dictionary<string, object> values = new Dictionary<string, object>();
                values.Add("controller", "questionattentions");
                values.Add("action", "Detail");
                values.Add("id", questionAttention.Id);
                return CreatedAtRoute("DefaultApi", values, questionAttention);
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE: api/QuestionAttentions/remove/5
        [ResponseType(typeof(void))]
        [ActionName("Remove")]
        [HttpDelete]
        public IHttpActionResult DeleteQuestionAttention(int questionId)
        {
            if (repo.RemoveQuestionAttention(UserManager.FindByName(User.Identity.Name).Id, questionId) == 1)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
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