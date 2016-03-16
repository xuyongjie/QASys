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


        // POST: api/QuestionAttentions
        [ResponseType(typeof(QuestionAttention))]
        public IHttpActionResult PostQuestionAttention(QuestionAttention questionAttention)
        {
            if (repo.CreateQuestionAttention(questionAttention) == 1)
            {
                return CreatedAtRoute("DefaultApi", new { id = questionAttention.Id }, questionAttention);
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE: api/QuestionAttentions/5
        [ResponseType(typeof(void))]
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