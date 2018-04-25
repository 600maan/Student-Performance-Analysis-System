using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SPMS.Models;

namespace SPMS.Controllers
{
    public class HomeController : Controller
    {

        private UsersContext db = new UsersContext();
        public ActionResult Index()
        {
            ViewBag.Message = "We help you monitor your students.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "About the school";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contacts and Feedback";

            return View();
        }

        public ActionResult Feedback()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Feedback(Feedback model)
        {
            try
            {
                db.Entry(model).State = EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Error Occured");
            }
        }
      }
}
