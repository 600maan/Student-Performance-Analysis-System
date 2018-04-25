using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SPMS.Models;

namespace SPMS.Controllers
{
     [Authorize]
    public class FeeController : Controller
    {
        private UsersContext db = new UsersContext();

        // GET: /Fee/
        public ActionResult Index()
        {
            return View(db.fee.ToList());
        }

        // GET: /Fee/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fee fee = db.fee.Find(id);
            if (fee == null)
            {
                return HttpNotFound();
            }
            return View(fee);
        }

        // GET: /Fee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Fee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="FeeId,ClassName,SecurityDeposit,LaboratoryCharges,SwimmingFee,MonthlyTutionFee,MonthlyHostelFee,MonthlyTransportationFee,AdmissionFee,BookFee,StationaryCharges")] Fee fee)
        {
            if (ModelState.IsValid)
            {
                db.fee.Add(fee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fee);
        }

        // GET: /Fee/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fee fee = db.fee.Find(id);
            if (fee == null)
            {
                return HttpNotFound();
            }
            return View(fee);
        }

        // POST: /Fee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="FeeId,ClassName,SecurityDeposit,LaboratoryCharges,SwimmingFee,MonthlyTutionFee,MonthlyHostelFee,MonthlyTransportationFee,AdmissionFee,BookFee,StationaryCharges")] Fee fee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fee);
        }

        // GET: /Fee/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fee fee = db.fee.Find(id);
            if (fee == null)
            {
                return HttpNotFound();
            }
            return View(fee);
        }

        // POST: /Fee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Fee fee = db.fee.Find(id);
            db.fee.Remove(fee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
