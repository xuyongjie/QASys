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

        // GET: api/nices/5
        [ResponseType(typeof(Nice))]
        public IHttpActionResult GetNice(int id)
        {
            return Ok(repo.GetNiceDetail(id));
        }

        // POST: api/Nices
        [ResponseType(typeof(Nice))]
        public IHttpActionResult PostNice(Nice nice)
        {
            if (repo.CreateNice(nice)==1)
            {
                return CreatedAtRoute("DefaultApi", new { id = nice.Id }, nice);
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE: api/Nices/5
        [ResponseType(typeof(void))]
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