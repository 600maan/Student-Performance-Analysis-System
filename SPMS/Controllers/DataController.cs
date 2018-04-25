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
    public class DataController : Controller
    {
        public UsersContext db = new UsersContext();
        static string batch = MvcApplication.GetBatch();
        //
        // GET: /Data/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult XStudents()
        {
            return PartialView("_XStudents");
        }

        public ActionResult GetXStudents(string batch)
        {
            var students = db.student.Where(m => m.EnrolledDate.Equals(batch)).ToList();
            return PartialView("_GetXStudents",students);
        }
        public ActionResult Details(int id)
        {
            Student st1 = db.student.Find(id);
            Parent pt1 = db.parent.Where(m => m.StdSchoolId == st1.StdSchoolId).First();
            RegistrationView r1 = new RegistrationView();
            r1.EnrolledDate = st1.EnrolledDate;
            r1.StudentFirstName = st1.StudentFirstName;
            r1.StudentMiddleName = st1.StudentMiddleName;
            r1.StudentLastName = st1.StudentLastName;
            r1.Street = st1.Street;
            r1.City = st1.City;
            r1.Country = st1.Country;
            r1.DateofBirth = st1.DateofBirth;
            r1.ImageUrl = st1.ImageUrl;
            r1.ClassId = st1.ClassId;
            r1.Gender = st1.Gender;
            r1.Rollno = st1.Rollno;
            r1.StdSchoolId = st1.StdSchoolId;
            r1.Attendance = st1.Attendance;//Parent p1 = db.parent.Find(id);

            r1.ParentFirstName = pt1.ParentFirstName;
            r1.ParentMiddleName = pt1.ParentMiddleName;
            r1.ParentLastName = pt1.ParentLastName;
            r1.Phone = pt1.Phone;
            r1.Email = pt1.Email;
            r1.Occupation = pt1.Occupation;
            r1.Relation = pt1.Relation;
            r1.StdSchoolId = pt1.StdSchoolId;
            return PartialView("_Details",r1);
        }
        public ActionResult DataAnalysis()
        {
            ViewBag.Classlist = new SelectList(db.classinfo.ToList(), "ClassId", "ClassName");
            return PartialView("_DataAnalysis");
        }
        public ActionResult ClasswiseAnalysis(int Classid)
        {
            ViewBag.classid = Classid;
            var results = db.result.Where(m => !m.exam.examnotification.Batch.Equals(batch) && m.exam.ClassId == Classid).ToList();
            var subjects = db.subject.Where(m => m.ClassId == Classid).ToList();
            Analysis analysis = db.analysis.Where(m => m.ClassId == Classid).First();
            List<long> subjectid = new List<long>();
            List<int> Average = new List<int>();
            List<int> failcount = new List<int>();
            foreach (var subject in subjects)
            {
                subjectid.Add(subject.SubjectId);
                int totalscore = 0;
                int count = 0;
                int fail = 0;
                var subresult = results.Where(m => m.exam.SubjectId == subject.SubjectId).ToList();
                foreach (var item in subresult)
                {
                    if (item.MarksObtained != 0)
                    {
                        totalscore += item.MarksObtained;
                        count++;
                    }
                    if (item.MarksObtained < 50)
                    {
                        fail++;
                    }
                }
                failcount.Add(fail);
                Average.Add(totalscore / count);
            }
            int max = failcount.Max();
            for (int i = 0; i < failcount.Count(); i++)
            {
                if (failcount[i] == max)
                {
                    ViewBag.weaksubject = db.subject.Find(subjectid[i]).SubjectName;
                }
            }
            analysis.Subject1 = subjectid[0];
            analysis.Average1 = Average[0];
            analysis.FailCount1 = failcount[0];
            analysis.Subject2 = subjectid[1];
            analysis.Average2 = Average[1];
            analysis.FailCount2 = failcount[1];
            analysis.Subject3 = subjectid[2];
            analysis.Average3 = Average[2];
            analysis.FailCount3 = failcount[2];
            analysis.Subject4 = subjectid[3];
            analysis.Average4 = Average[3];
            analysis.FailCount4 = failcount[3];
            try
            {
                db.Entry(analysis).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch
            {
                Content("Something went wrong...");
            }
            return PartialView("_ClasswiseAnalysisResult");
        }
        public ActionResult OverallAnalysis()
        {
            var classlist = db.classinfo.ToList();
            foreach (var item in classlist)
            {
                int id = (int)item.ClassId;
                AnalysisClass(id);
            }
            return PartialView("_OverallAnalysis");
        }
        
        
        
        
        
        
        
        
        
        public void AnalysisClass(int Classid)
        {
            var results = db.result.Where(m => !m.exam.examnotification.Batch.Equals(batch) && m.exam.ClassId == Classid).ToList();
            var subjects = db.subject.Where(m => m.ClassId == Classid).ToList();
            Analysis analysis = db.analysis.Where(m => m.ClassId == Classid).First();
            List<long> subjectid = new List<long>();
            List<int> Average = new List<int>();
            List<int> failcount = new List<int>();
            foreach (var subject in subjects)
            {
                subjectid.Add(subject.SubjectId);
                int totalscore = 0;
                int count = 0;
                int fail = 0;
                var subresult = results.Where(m => m.exam.SubjectId == subject.SubjectId).ToList();
                foreach (var item in subresult)
                {
                    if (item.MarksObtained != 0)
                    {
                        totalscore += item.MarksObtained;
                        count++;
                    }
                    if (item.MarksObtained < 50)
                    {
                        fail++;
                    }
                }
                failcount.Add(fail);
                Average.Add(totalscore / count);
            }
            int max = failcount.Max();
            for (int i = 0; i < failcount.Count(); i++)
            {
                if (failcount[i] == max)
                {
                    ViewBag.weaksubject = subjectid[i];
                }
            }
            analysis.Subject1 = subjectid[0];
            analysis.Average1 = Average[0];
            analysis.FailCount1 = failcount[0];
            analysis.Subject2 = subjectid[1];
            analysis.Average2 = Average[1];
            analysis.FailCount2 = failcount[1];
            analysis.Subject3 = subjectid[2];
            analysis.Average3 = Average[2];
            analysis.FailCount3 = failcount[2];
            analysis.Subject4 = subjectid[3];
            analysis.Average4 = Average[3];
            analysis.FailCount4 = failcount[3];
            try
            {
                db.Entry(analysis).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch
            {
                Content("Something went wrong...");
            }
        }
    }
}
