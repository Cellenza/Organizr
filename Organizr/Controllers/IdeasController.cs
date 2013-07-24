using Organizr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Organizr.Controllers
{
   [Authorize]
    public class IdeasController : Controller
    {
        OrganizrContext context;

        public IdeasController()
        {
            context = new OrganizrContext();
        }

        //
        // GET: /Ideas/
        public ActionResult Index()
        {
            return View(context.Ideas.ToArray());
        }

        public ActionResult Create()
        {
            return View();
        }

       public ActionResult Details(int id)
        {
            return View(context.Ideas.Single(i => i.Id == id));
        }

        [HttpPost]
        public ActionResult Create(Idea idea)
        {
            var currentUser = context.Users.Single(u => u.UserName.Equals(User.Identity.Name));
            context.Ideas.Add(idea);
            idea.Submitter = currentUser;
            idea.Submitted = DateTime.Now;
            var errors = context.GetValidationErrors();
            if (errors.Any())
            {
                return View();
            }
            else
            {
                context.SaveChanges();
                return Redirect("Index");
            }
            
        }
	}
}