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
    public class AttendanceController : Controller
    {
        public UsersContext db = new UsersContext();
        static string batch = MvcApplication.GetBatch();
        //
        // GET: /Attendance/
        public ActionResult AutoAttendance()
        {
            var data = db.student.ToList();
            DateTime date1 =new DateTime(2014,5,1) ;
            DateTime date2 = new DateTime(2014, 8, 31);
            for (int i = 0; date1 <= date2; i++)
            {
                if (date1.DayOfWeek.ToString()=="Saturday" )
                {
                    date1 = date1.AddDays(1);
                    continue;
                }
                foreach (var item in data)
                { 
                Attendance attendance = new Attendance();
                attendance.ClassId = item.ClassId;
                attendance.AttendanceStatus = true;
                attendance.StudentId = item.StudentId;
                attendance.Date = date1;
                if (item.StudentMiddleName == null)
                {
                    attendance.StudentName = item.StudentFirstName + " " + item.StudentLastName;
                }
                else
                {
                    attendance.StudentName = item.StudentFirstName + " " + item.StudentMiddleName + " " + item.StudentLastName;
                }
                db.Entry(attendance).State = EntityState.Added;
                db.SaveChanges();
                }
             date1=date1.AddDays(1);
            }

                return RedirectToAction("Index");
        }

        public ActionResult CorrectAttendance()
        {
            var data = db.student.ToList();
            foreach (var item in data)
            {
                int absentdays=105-item.Attendance;
                for (int i = 0; i<absentdays; i++)
                {
                    Random ran = new Random();
                    int date1day = ran.Next(1,31);
                    int date1month = ran.Next(5,9);
                    DateTime dt = new DateTime(2014, date1month, date1day);
                    if (dt.DayOfWeek.ToString() == "Saturday")
                    {
                        i--;
                        continue;
                    }
                    var att = db.attendance.Where(m => m.StudentId == item.StudentId && m.Date == dt).First();
                    Attendance att1 = att;
                    if (att1.AttendanceStatus == false)
                    {
                        i--;
                        continue;
                    }
                    att1.AttendanceStatus = false;
                    db.Entry(att1).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return View();
        }

        public ActionResult Index()
        {
            ViewBag.DropdownClass = new SelectList(db.classinfo.ToList(),"ClassId","ClassName");
            return View();
        }
      [HttpPost]
        public ActionResult Index(int DropdownClass)
        {
          List<Attendance> abc=new List<Attendance>();
          abc= db.attendance.Where(s=>s.ClassId==DropdownClass && s.Date==DateTime.Today).ToList();
            if (abc.Count==0)
            {
                return RedirectToAction("TakeAttendance", "Attendance", new { DropdownClass });
            }
            else
            {
                return RedirectToAction("Edit", new { DropdownClass , date=DateTime.Today});
            }
        }
        public ActionResult TakeAttendance(int DropdownClass)
        {

            List<Student> listStudent = new List<Student>();
            List<Attendance> lstatt = new List<Attendance>();
            listStudent = db.student.Where(m => m.ClassId == DropdownClass && m.EnrolledDate.Equals(batch)).ToList();
            foreach (Student item in listStudent)
            {
                Attendance list1 = new Attendance();
                list1.ClassId = item.ClassId;
                list1.StudentId = item.StudentId;
                list1.Date = DateTime.Today;
                if (item.StudentMiddleName == null)
                {
                    list1.StudentName = item.StudentFirstName + " " + item.StudentLastName;
                }
                else
                {
                    list1.StudentName = item.StudentFirstName + " " + item.StudentMiddleName + " " + item.StudentLastName;
                }
                lstatt.Add(list1);
            }

            return View(lstatt);
        }
        [HttpPost]
        public ActionResult TakeAttendance(List<Attendance> listatt)
        {
             
            for (int i = 0; i < listatt.Count;i++ )
            {
                if (listatt[i].AttendanceStatus)
                {
                    Student student1=new Student();
                    student1 = db.student.Find(listatt[i].StudentId);
                    student1.Attendance += 1;
                    db.Entry(student1).State = EntityState.Modified;
                    db.SaveChanges();
                }

                try
                {
                    db.Entry(listatt[i]).State = EntityState.Added;
                    db.SaveChanges();

                }
                catch
                {
                    return View("AttendanceTaken");
                }
            }
            return RedirectToAction("AttendanceTaken", "Attendance", new {DropdownClass=listatt[0].ClassId,date=listatt[0].Date });
        }

        public ActionResult AttendanceTaken(int DropdownClass,DateTime date)
        {
            var data = from m in db.attendance where(m.Date == date && m.ClassId == DropdownClass) select m;
            return View(data);
        }

        //
        // GET: /Attendance/Edit/5

        public ActionResult Edit(int DropdownClass,DateTime date)
        {
            List<Attendance> obj1 = new List<Attendance>();
            obj1 = db.attendance.Where(s=>s.ClassId==DropdownClass && s.Date==date).ToList();
            return View(obj1);
        }

        //
        // POST: /Attendance/Edit/5

        [HttpPost]
        public ActionResult Edit(List<Attendance> listatt)
        {
             
        
            for (int i = 0; i < listatt.Count; i++)
            {
                Attendance prelist= db.attendance.Find(listatt[i].AttendanceId);
                if (prelist.AttendanceStatus)
                {
                    if (!listatt[i].AttendanceStatus)
                    {
                        Student student1 = new Student();
                        student1 = db.student.Find(listatt[i].StudentId);
                        student1.Attendance -= 1;
                        db.Entry(student1).State = EntityState.Modified;
                        db.SaveChanges();
                       
                    }
                }
                else
                {
                    if (listatt[i].AttendanceStatus)
                    {
                        Student student1 = new Student();
                        student1 = db.student.Find(listatt[i].StudentId);
                        student1.Attendance += 1;
                        db.Entry(student1).State = EntityState.Modified;
                        db.SaveChanges();
                     

                    }
                   
                }
                try
                {
                    prelist.AttendanceStatus = listatt[i].AttendanceStatus;
                    db.Entry(prelist).State = EntityState.Modified;
                    db.SaveChanges();

                }
                catch
                {
                    return View("AttendanceTaken", listatt);
                }


            }
            return RedirectToAction("AttendanceTaken", "Attendance", listatt[0]);
        }

        //
        // GET: /Attendance/Delete/5

        public ActionResult Delete(int id)
        {
            Attendance obj2 = db.attendance.Find(id);
            return View();
        }

        //
        // POST: /Attendance/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, Attendance model)
        {
            try
            {
                // TODO: Add delete logic here
                Attendance obj2 = db.attendance.Find(id);
                db.attendance.Remove(obj2);
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
