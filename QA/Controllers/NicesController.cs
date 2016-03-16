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
    public class NicesController : ApiController
    {
        private readonly INiceRepository repo = new NiceRepository();

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

        // GET: api/Nices/detail/5
        [ResponseType(typeof(Nice))]
        [ActionName("Detail")]
        [HttpGet]
        public IHttpActionResult GetNice(int id)
        {
            return Ok(repo.GetNice(id));
        }

        // POST: api/Nices/create
        [ResponseType(typeof(Nice))]
        [ActionName("Create")]
        [HttpPost]
        public IHttpActionResult PostNice(Nice nice)
        {
            if (repo.CreateNice(nice)==1)
            {
                Dictionary<string, object> values = new Dictionary<string, object>();
                values.Add("controller", "nices");
                values.Add("action", "Detail");
                values.Add("id", nice.Id);
                return CreatedAtRoute("DefaultApi", values, nice);
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE: api/Nices/remove/5
        [ResponseType(typeof(void))]
        [ActionName("Remove")]
        [HttpDelete]
        public IHttpActionResult DeleteNice(int answerId)
        {
            if (repo.RemoveNice(UserManager.FindByName(User.Identity.Name).Id, answerId) == 1)
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