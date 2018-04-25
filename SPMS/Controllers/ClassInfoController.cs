using SPMS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SPMS.Controllers
{
     [Authorize]
    public class ClassInfoController : Controller
    {
        public UsersContext db = new UsersContext();
        //
        // GET: /Class/

        public ActionResult Index()
        {
            return View(db.classinfo.ToList());
        }

        //
        // GET: /Class/Details/5

      
        //
        // GET: /Class/Create

        public ActionResult Create()
        {
           ViewBag.TeacherId = new SelectList(db.teacher.ToList(),"TeacherId","TeacherFirstName");
            return View();
        }

        //
        // POST: /Class/Create

        [HttpPost]
        public ActionResult Create(ClassInfo model)
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
        // GET: /Class/Edit/5

        public ActionResult Edit(int id)
        {

            ViewBag.TeacherId = new SelectList(db.teacher.ToList(), "TeacherId", "TeacherFirstName");
         
            ClassInfo obj = db.classinfo.Find(id);
            return View(obj);
        }

        //
        // POST: /Class/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, ClassInfo model)
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
        // GET: /Class/Delete/5

        public ActionResult Delete(int id)
        {
            ClassInfo obj1 = db.classinfo.Find(id);

            return View(obj1);
        }

        //
        // POST: /Class/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, ClassInfo model)
        {
            try
            {
                // TODO: Add delete logic here
                ClassInfo obj1 = db.classinfo.Find(id);
                db.classinfo.Remove(obj1);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult GetClassList()
        {
            UsersContext db = new UsersContext();
            return PartialView("_GetClassList", db.classinfo.ToList());
        }
      
    }
}
