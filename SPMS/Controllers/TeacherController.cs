 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SPMS.Models;
using System.Data;
using System.Data.Entity;

namespace SPMS.Controllers
{
     [Authorize]
    public class TeacherController : Controller
    {
        public UsersContext db = new UsersContext();
        //
        // GET: /Teacher/

        public ActionResult Index()
        {
            return View(db.teacher.ToList());
        }

        //
        // GET: /Teacher/Details/5

        public ActionResult Details(int id)
        {
            Teacher obj = db.teacher.Find(id);
            return View(obj);
        }

        //
        // GET: /Teacher/Create

        public ActionResult Create()
        {
            ViewBag.SubjectId = new SelectList(db.subject.ToList(), "SubjectId", "SubjectName");
            return View();
        }

        //
        // POST: /Teacher/Create

        [HttpPost]
        public ActionResult Create(Teacher model)
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
        // GET: /Teacher/Edit/5

        public ActionResult Edit(int id)
        {
            Teacher obj = db.teacher.Find(id);
            return View(obj);
        }

        //
        // POST: /Teacher/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Teacher model)
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
        // GET: /Teacher/Delete/5

        public ActionResult Delete(int id)
        {
            Teacher obj = db.teacher.Find(id);
            return View(obj);
        }

        //
        // POST: /Teacher/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, Teacher model)
        {
            try
            {
                // TODO: Add delete logic here
                Teacher obj = db.teacher.Find(id);
                db.teacher.Remove(obj);
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
