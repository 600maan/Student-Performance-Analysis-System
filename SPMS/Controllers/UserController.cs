using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SPMS.Models;
using System.Web.Helpers;

namespace SPMS.Controllers
{
    public class UserController : Controller
    {
        private UsersContext db = new UsersContext();
        public static int studentList = 0;
        // GET: /User/

        public ActionResult ExamChart()
        {
            List<ChartData> examdata = new List<ChartData>();
            List<Result> result = new List<Result>();
            List<string> series = new List<string>();
            result = db.result.Where(m => m.StudentId == studentList).ToList();
            long classid = db.student.Find(studentList).ClassId;
            var subject = db.subject.Where(m => m.ClassId == classid).ToList();
            string[] exam = new string[6] { "Unit Test 1", "First Terminal", "Unit Test 2", "Second Terminal", "Unit Test 3", "Third Terminal" };
            for (int i = 0; i < 6; i++)
            {
                List<int> value = new List<int>();
                foreach (var subitem in subject)
                {
                    foreach (var item in result)
                    {
                        if (item.exam.ExamType == exam[i] && item.exam.SubjectId == subitem.SubjectId)
                        {
                            value.Add(item.MarksObtained);
                        }
                    }
                    series.Add(subitem.SubjectName);
                }
                examdata.Add(new ChartData(exam[i], value[0], value[1], value[2], value[3]));
            }
            ViewBag.series = series;
            return PartialView("_ExamChart", examdata);
        }

        public ActionResult VChat()
        {
            DateTime sdate = DateTime.Today;
            Meeting mdate = db.meeting.Where(m => m.MeetDate >= sdate).First();
            var rdays = mdate.MeetDate - sdate;
            if (sdate.Date == mdate.MeetDate.Date)
            {

                if (studentList == 0)
                {
                    var teacherlist = db.teacher.ToList();
                    long teacherid = db.teacher.Find(teacherlist).TeacherId;
                    var tdata = db.teacher.Where(m => m.TeacherId == teacherid).ToList();
                    string tname = "Teacher:" + tdata[0].TeacherFirstName + " " + tdata[0].TeacherLastName;
                    ViewBag.name = tname;
                }
                else
                {

                    long stdschoolid = db.student.Find(studentList).StdSchoolId;
                    var data = db.parent.Where(m => m.StdSchoolId == stdschoolid).ToList();
                    string name = "Parent:" + data[0].ParentFirstName + " " + data[0].ParentLastName;
                    ViewBag.name = name;
                }

                return View();
            }

            else
            {
                return Content("Meeting is on: " + rdays + "days");

            }
        }



        public ActionResult Othernotice()
        {
            List<Notification> notice = db.notification.ToList();
            for (int i = 0; i < notice.Count(); i++)
            {
                if (notice[i].ValidUpto < System.DateTime.Today)
                {
                    notice.Remove(notice[i]);
                }
            }
            return PartialView("_notice", notice);
        }
        public ActionResult Noticeforresult()
        {

            List<ExamNotification> pubdate = db.examnotification.ToList();
            for (int i = 0; i < pubdate.Count(); i++)
            {
                if (pubdate[i].ResultDate < System.DateTime.Today)
                {
                    pubdate.Remove(pubdate[i]);
                }
            }
            return PartialView("_Noticeforresult", pubdate);
        }





        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Parent model)
        {
            try
            {

                List<Parent> p = (db.parent.Where(r => r.StdSchoolId == model.StdSchoolId)).ToList();
                if (p[0].UserPassword == model.UserPassword)
                {
                    MvcApplication.logcheck = 1;
                    return RedirectToAction("Home", new { id = p[0].StdSchoolId });
                }
                else { return Content("Inavlid User Name or Password"); }
            }
            catch { return Content("Invalid User Name"); }

            //return RedirectToAction("Home", new { id = model.StdSchoolId });

        }
        public ActionResult Home(long id)
        {
            ViewBag.message = id;
            List<Student> stud = (db.student.Where(r => r.StdSchoolId == id)).ToList();
            Student student = stud[0];
            studentList = (int)student.StudentId;
         
            if ((int)id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        public ActionResult GetChart(int? subject)
        {

            List<ChartData> chartdata = new List<ChartData>();
            List<PerformanceIndex> assignmentData = new List<PerformanceIndex>();
            List<Result> examData = new List<Result>();
            int[] yval = new int[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            string[] xval = new string[12] { "", "", "", "", "", "", "", "", "", "", "", "" };
            if (subject == null)
            {
                assignmentData = db.performanceindex.Where(m => m.StudentId == studentList).ToList();
                examData = db.result.Where(m => m.StudentId == studentList).ToList();
                ViewBag.sname="Overall Progress Graph";
            }
            else
            {
                assignmentData = db.performanceindex.Where(m => m.StudentId == studentList && m.assignment.SubjectId == subject).ToList();
                examData = db.result.Where(m => m.StudentId == studentList && m.exam.SubjectId == subject).ToList();
                ViewBag.sname = examData[0].exam.SubjectName+" Progress Graph";
            }
            for (int j = 1; j <= 12; j++)
            {
                int avg = 0;
                int count = 0;
                int year = 0;
                for (int i = 0; i < assignmentData.Count(); i++)
                {
                    if (assignmentData[i].assignment.ToSubmitDate.Month == j)
                    {
                        if (year == 0)
                        {
                            year = assignmentData[i].assignment.ToSubmitDate.Year;
                        }
                        if (assignmentData[i].assignment.ToSubmitDate.Year == year)
                        {
                            avg += assignmentData[i].MarksObtained * 5;
                            count++;
                        }
                    }
                }
                for (int k = 0; k < examData.Count(); k++)
                {
                    if (year == 0 && examData[k].exam.examnotification.ResultDate.Month == j)
                    {
                        year = examData[k].exam.examnotification.ResultDate.Year;
                    }
                    if (examData[k].exam.examnotification.ResultDate.Month == j && examData[k].exam.examnotification.ResultDate.Year == year)
                    {
                        avg += examData[k].MarksObtained;
                        count++;
                    }
                }
                if (count != 0)
                {
                    yval[j - 1] = avg / count;
                }
                else
                {
                    yval[j - 1] = 0;
                }
                xval[j - 1] = "" + year + "/" + j;
            }
            int y = 4;
            int[] tempy = new int[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            string[] tempx = new string[12] { "", "", "", "", "", "", "", "", "", "", "", "" };
            for (int i = 0; i < 12; i++)
            {
                tempy[i] = yval[y];
                tempx[i] = xval[y];
                y++;
                if (y == 12)
                {
                    y = 0;
                }
            }
            yval = tempy;
            xval = tempx;
            for (int i = 0; i < 12; i++)
            {
                chartdata.Add(new ChartData(xval[i], yval[i]));
            }

            // graph display
            var chart = new Chart(400, 400, ChartTheme.Vanilla)
              .AddSeries(
                        chartType: "line",
                        xValue: xval,
                         yValues: yval);

            var rr = new Random();
            ViewBag.GraphKey = rr.Next().ToString();
            chart.SaveToCache(ViewBag.GraphKey, 1, false);
            return PartialView("_GetChart", chartdata);
        }

        public ActionResult GetClassExam()
        {
            string[] examtype = new string[6] { "Unit Test 1", "First Terminal", "Unit Test 2", "Second Terminal", "Unit Test 3", "Third Terminal" };
            ViewBag.et = examtype;
            ViewBag.studentList = studentList;
            Student s = db.student.Find(studentList);
            ViewBag.classlist = s.ClassId;
            return PartialView("_GetClassExam");
        }
        public ActionResult GetClassMarkSheet()
        {
            Student s = db.student.Find(studentList);
            long cid = s.ClassId;

            return PartialView("_GetClassExam");
        }

        public ActionResult GetAssignment()
        {
            List<PerformanceIndex> pindex = new List<PerformanceIndex>();

            pindex = db.performanceindex.Where(m => m.StudentId == studentList).ToList();
            return PartialView("_GetAssignment", pindex);
        }
        public ActionResult GetAttendance()
        {
            List<ChartData> chartdata = new List<ChartData>();
            int presentdays = db.student.Find(studentList).Attendance;
            int totalschooldays = db.attendance.Where(m => m.StudentId == studentList).Count();
            chartdata.Add(new ChartData("Present Days", presentdays));
            chartdata.Add(new ChartData("Absent Days", totalschooldays - presentdays));
            return PartialView("_GetAttendanceChart", chartdata);
        }

        public ActionResult GetSubject()
        {
            var data = db.student.Find(studentList);
            return PartialView("_GetSubject", db.subject.Where(m => m.ClassId == data.ClassId));
        }

        public ActionResult CustomReport(string key)
        {
            var chart = Chart.GetFromCache(key);
            chart.Write("png");
            return null;
        }

        public ActionResult GetAbsentDays()
        {
            var data = db.attendance.Where(m => m.StudentId == studentList && m.AttendanceStatus == false).ToList();
            return PartialView("_GetAbsentDays", data);
        }

        public ActionResult GetExam()
        {
            string[] examtype = new string[6] { "Unit Test 1", "First Terminal", "Unit Test 2", "Second Terminal", "Unit Test 3", "Third Terminal" };
            ViewBag.et = examtype;
            ViewBag.studentList = studentList;
            return PartialView("_GetExam");
        }


        // GET: /User/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parent parent = db.parent.Find(id);
            if (parent == null)
            {
                return HttpNotFound();
            }
            return View(parent);
        }

        // GET: /User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ParentId,PrntId,ParentFirstName,ParentMiddleName,ParentLastName,Phone,Email,UserPassword,Relation,Occupation")] Parent parent)
        {
            if (ModelState.IsValid)
            {
                db.parent.Add(parent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(parent);
        }

        // GET: /User/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parent parent = db.parent.Find(id);
            if (parent == null)
            {
                return HttpNotFound();
            }
            return View(parent);
        }

        // POST: /User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ParentId,PrntId,ParentFirstName,ParentMiddleName,ParentLastName,Phone,Email,UserPassword,Relation,Occupation")] Parent parent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(parent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(parent);
        }

        // GET: /User/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parent parent = db.parent.Find(id);
            if (parent == null)
            {
                return HttpNotFound();
            }
            return View(parent);
        }

        // POST: /User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Parent parent = db.parent.Find(id);
            db.parent.Remove(parent);
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
