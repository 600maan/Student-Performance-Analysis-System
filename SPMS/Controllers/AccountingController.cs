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
    public class AccountingController : Controller
    {
        private UsersContext db = new UsersContext();

        public ActionResult Index()
        {
            return View("AccountingHome");
        }
        [HttpPost]
        public ActionResult Index(Accounting model)
        {
            Accounting acc = new Accounting();
            int sid =  model.StdSchoolId;
            //Student sss = new Student();

            List<Student> student1 = new List<Student>();
            student1 = db.student.Where(s => s.StdSchoolId == sid).ToList();
            long stclass = student1[0].ClassId;
            Fee stfee = db.fee.Find(stclass);
            ViewBag.date = DateTime.Now;
            ViewBag.stscid = sid;
            ViewBag.nm = student1[0].StudentFirstName + " " + student1[0].StudentMiddleName + " " + student1[0].StudentLastName;
            ViewBag.cl = student1[0].ClassId;
            ViewBag.adfee ="Rs "+ stfee.AdmissionFee;
            ViewBag.sd = "Rs " + stfee.SecurityDeposit;
            ViewBag.lc = "Rs " + stfee.LaboratoryCharges;
            ViewBag.sc = "Rs " + stfee.SwimmingFee;
            ViewBag.b = "Rs " + stfee.BookFee;
            ViewBag.s = "Rs " + stfee.StationaryCharges;
            ViewBag.mtf = "Rs " + stfee.MonthlyTutionFee;
            ViewBag.mhf = "Rs " + stfee.MonthlyHostelFee;
            ViewBag.mtrf = "Rs " + stfee.MonthlyTransportationFee;
            var total = stfee.AdmissionFee + stfee.BookFee + stfee.LaboratoryCharges + (stfee.MonthlyTutionFee *12) + stfee.SwimmingFee + stfee.StationaryCharges + (stfee.MonthlyHostelFee * 12);
            ViewBag.tt = " Rs " + total;
            ViewBag.bal= "Rs " + acc.Balance;
            ViewBag.due = "Rs " + acc.Due;
            ViewBag.billamttoday = "Rs " ;
            return View();
        }
        // GET: /Accounting/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accounting accounting = db.accounting.Find(id);
            if (accounting == null)
            {
                return HttpNotFound();
            }
            return View(accounting);
        }

        // GET: /Accounting/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Accounting/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="AccId,Date,ClassName,Rollno,StdSchoolId,TobePaid,PaidAmount,Due")] Accounting accounting)
        {
            if (ModelState.IsValid)
            {
                db.accounting.Add(accounting);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(accounting);
        }

        // GET: /Accounting/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accounting accounting = db.accounting.Find(id);
            if (accounting == null)
            {
                return HttpNotFound();
            }
            return View(accounting);
        }

        // POST: /Accounting/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="AccId,Date,ClassName,Rollno,StdSchoolId,TobePaid,PaidAmount,Due")] Accounting accounting)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accounting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accounting);
        }

        // GET: /Accounting/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accounting accounting = db.accounting.Find(id);
            if (accounting == null)
            {
                return HttpNotFound();
            }
            return View(accounting);
        }

        // POST: /Accounting/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Accounting accounting = db.accounting.Find(id);
            db.accounting.Remove(accounting);
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
