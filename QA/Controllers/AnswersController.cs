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
using Entity.EntityDTO;
using Microsoft.AspNet.Identity;

namespace QA.Controllers
{
    public class AnswersController : ApiController
    {
        private readonly IAnswerRepository repo = new AnswerRepository();

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

        // GET: api/answers/detail/5
        [ResponseType(typeof(AnswerDTO))]
        [ActionName("Detail")]
        [HttpGet]
        public IHttpActionResult GetAnswer(int id)
        {
            return Ok(repo.GetAnswer(id));
        }

        // POST: api/answers/create
        [ResponseType(typeof(AnswerDTO))]
        [ActionName("Create")]
        [HttpPost]
        public IHttpActionResult PostAnswer(Answer answer)
        {
            if (repo.CreateAnswer(answer) == 1)
            {
                Dictionary<string, object> values = new Dictionary<string, object>();
                values.Add("controller", "answers");
                values.Add("action", "Detail");
                values.Add("id", answer.Id);
                return CreatedAtRoute("DefaultApi", values, answer);
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE: api/answers/remove/5
        [ResponseType(typeof(void))]
        [ActionName("Remove")]
        [HttpDelete]
        public IHttpActionResult DeleteAnswer(int answerId)
        {
            if (repo.RemoveAnswer(UserManager.FindByName(User.Identity.Name).Id, answerId) == 1)
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