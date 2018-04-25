using SPMS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SPMS.Controllers
{
     [Authorize]
    public class SubjectController : Controller
    {
        public UsersContext db = new UsersContext();
        //
        // GET: /Subject/

        public ActionResult Index()
        {

            return View(db.subject.ToList());
        }

       

       

        //
        // GET: /Subject/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Subject/Create

        [HttpPost]
        public ActionResult Create(Subject model)
        {
            try
            {

                // TODO: Add insert logic here
                db.Entry(model).State = EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Subject/Edit/5

        public ActionResult Edit(int id)
        {
            Subject obj = db.subject.Find(id);
            return View(obj);
        }

        //
        // POST: /Subject/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Subject model)
        {
            try
            {
                // TODO: Add update logic here
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Subject/Delete/5

        public ActionResult Delete(int id)
        {
            Subject obj1 = db.subject.Find(id);
            return View(obj1);
        }

        //
        // POST: /Subject/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, Subject model)
        {
            try
            {
                // TODO: Add delete logic here
                Subject obj2 = db.subject.Find(id);
                db.subject.Remove(obj2);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
