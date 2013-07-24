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
            return View(context.Ideas.Include("Submitter").ToArray());
        }

        public ActionResult Create()
        {
            return View();
        }

       public ActionResult Details(int id)
       {
           var idea = context.Ideas.Include("Submitter").SingleOrDefault(i => i.Id == id);
           if (idea == null)
           {
               TempData["InfoMessage"] = "This idea does not exists yet... Do you want to create a new one?";
               return RedirectToAction("Create");
           };
            return View(idea);
        }

        [HttpPost]
        public ActionResult Create(Idea idea)
        {
            var currentUser = context.Users.Single(u => u.UserName.Equals(User.Identity.Name));
            context.Ideas.Add(idea);
            idea.Submitter = currentUser;
            idea.Submitted = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return View(idea);
            }
            else
            {
                context.SaveChanges();
                return RedirectToAction("Details", new {id = idea.Id});
            }
            
        }
	}
}