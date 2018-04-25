using SPMS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
namespace SPMS.Controllers
{
    public class ExamController : Controller
    {

        public UsersContext db = new UsersContext();
        static string batch = MvcApplication.GetBatch();
            
        public ActionResult Index()
        {
            return View();
        }
        public long totalMarksInEachExam(long classList, string examType, int studentList) {
            long total = 0;
            // List of all the exams for this particular class and exam type i.e 4 exams science, math, social,english
            List<Exam> exams = db.exam.Where(m => m.ExamType == examType && m.ClassId == classList).ToList();
            foreach (var exam in exams)
            {
                List<Result> result = db.result.Where(m=>m.ExamId == exam.ExamId && m.StudentId == studentList).ToList();
                total += result[0].MarksObtained;
            }
            return total;
         }
        public List<long> GetRank(long classList, string ExamType)
        {
            //Result of all student of class==classlist in examtype of this batch
            var data = db.result.Where(m => m.exam.ClassId == classList && m.exam.examnotification.Batch==batch && m.exam.ExamType == ExamType).ToList();
            List<int> totalscore = new List<int>();
            List<Student> student=new List<Student>();
            student = db.student.Where(m => m.ClassId == classList && m.EnrolledDate.Equals(batch)).ToList();
            int i = 0;
            foreach(var item in student)
            {
                totalscore.Add(0);
                var studentdata = data.Where(m => m.StudentId == item.StudentId).ToList();
                foreach(var res in studentdata)
                {
                        totalscore[i] += res.MarksObtained;
                }
                i++;
            }
            int k = 0; 
            List<long> rank = new List<long>();
            List<int> temp = totalscore;
            for (int a = 0; a < totalscore.Count(); a++)
            {
                int max = temp.Max();
                for (int b = 0; b < totalscore.Count(); b++)
                {
                    if (totalscore[b] == max)
                    {
                        totalscore[b] = 0;
                        rank.Add(student[b].StudentId);
                        k++;
                        break;
                    }
                }

            }
            return rank;
        }


        //public List<long> GetRank(long classList, string examType, int studentList)
        //{


        //    //Result of all student of class==classlist in examtype of this batch
        //    var data = db.result.Where(m => m.exam.ClassId == classList && m.exam.examnotification.Batch == batch && m.exam.ExamType == ExamType).ToList();
        //    List<int> totalscore = new List<int>();
        //    List<Student> student = new List<Student>();
        //    student = db.student.Where(m => m.ClassId == classList && m.EnrolledDate.Equals(batch)).ToList();
        //    int i = 0;
        //    foreach (var item in student)
        //    {
        //        totalscore.Add(0);
        //        var studentdata = data.Where(m => m.StudentId == item.StudentId).ToList();
        //        foreach (var res in studentdata)
        //        {
        //            totalscore[i] += res.MarksObtained;
        //        }
        //        i++;
        //    }
        //    int k = 0;
        //    List<long> rank = new List<long>();
        //    List<int> temp = totalscore;
        //    for (int a = 0; a < totalscore.Count(); a++)
        //    {
        //        int max = temp.Max();
        //        for (int b = 0; b < totalscore.Count(); b++)
        //        {
        //            if (totalscore[b] == max)
        //            {
        //                totalscore[b] = 0;
        //                rank.Add(student[b].StudentId);
        //                k++;
        //                break;
        //            }
        //        }

        //    }
        //    return rank;
        //}
        [AllowAnonymous]
        public ActionResult UResultByClass(int classList, string ExamType)
        {
            List<long> rank = GetRank(classList, ExamType);
            ViewBag.rank = rank;
            ViewBag.no = rank.Count();
            return PartialView("_ClassResult", db.result.Where(m => m.exam.ClassId == classList&&m.exam.examnotification.Batch==batch && m.exam.ExamType == ExamType).ToList());
        }


        public ActionResult ResultByClass()
        {
            System.Collections.Generic.List<SelectListItem> Examtype = new List<SelectListItem>();
            Examtype.Add(new SelectListItem { Text = "Unit Test 1", Value = "Unit Test 1" });
            Examtype.Add(new SelectListItem { Text = "First Terminal", Value = "First Terminal" });
            Examtype.Add(new SelectListItem { Text = "Unit Test 2", Value = "Unit Test 2" });
            Examtype.Add(new SelectListItem { Text = "Second Terminal", Value = "Second Terminal" });
            Examtype.Add(new SelectListItem { Text = "Unit Test 3", Value = "Unit Test 3" });
            Examtype.Add(new SelectListItem { Text = "Third Terminal", Value = "Third Terminal" });
            ViewBag.ExamType = new SelectList(Examtype, "Value", "Text", "Select one");
            ViewBag.classList = new SelectList(db.classinfo.ToList(), "ClassId", "ClassName");
            return View();
        }

        [HttpPost]
        public ActionResult ResultByClass(int classList, string ExamType)
        {
            List<Result> results  = new List<Result>();
            List<Subject> subjectlist = db.subject.Where(m=> m.ClassId == classList).ToList();
            ViewBag.subjects = subjectlist;
            List<Student> studentlist = db.student.Where(m=> m.ClassId == classList).ToList();
            ViewBag.students = studentlist;
            ViewBag.numberOfStudents = studentlist.Count();
            List<Exam> exams = db.exam.Where(m=> m.ClassId == classList && m.ExamType.Equals(ExamType)).ToList();
            foreach (Exam exam in exams) {
             List<Result> examresults = db.result.Where(m => m.ExamId == exam.ExamId).ToList();
             results.AddRange(examresults);
            }
            
            return PartialView("_ClassResult", results);
        }


        public ActionResult ResultByStudent()
        {
            return View();
        }


        public ActionResult GetStudentList(int classList)
        {
            System.Collections.Generic.List<SelectListItem> Examtype = new List<SelectListItem>();
            Examtype.Add(new SelectListItem { Text = "Unit Test 1", Value = "Unit Test 1" });
            Examtype.Add(new SelectListItem { Text = "First Terminal", Value = "First Terminal" });
            Examtype.Add(new SelectListItem { Text = "Unit Test 2", Value = "Unit Test 2" });
            Examtype.Add(new SelectListItem { Text = "Second Terminal", Value = "Second Terminal" });
            Examtype.Add(new SelectListItem { Text = "Unit Test 3", Value = "Unit Test 3" });
            Examtype.Add(new SelectListItem { Text = "Third Terminal", Value = "Third Terminal" });
            ViewBag.ExamType = new SelectList(Examtype, "Value", "Text", "Select one");
            
                ViewBag.studentList = new SelectList(db.student.Where(m => m.ClassId == classList&&m.EnrolledDate.Equals(batch)).Select(m => new { studentid = m.StudentId, name = m.StudentFirstName + " " + m.StudentMiddleName + " " + m.StudentLastName }).ToList(), "studentid", "name");
            return PartialView("_GetStudentList");
        }


        public ActionResult GetClassList()
        {
            ViewBag.classList = new SelectList(db.classinfo.ToList(), "ClassId", "ClassName");
            return PartialView("_GetClassList");
        }

        [AllowAnonymous]
        public ActionResult ShowResult(int studentList, string ExamType)
        {
            double marksobtained = 0.0;
            double total = 0.0;
            long classList = db.student.Find(studentList).ClassId;
            List<Result> results = db.result.Where(m => m.StudentId == studentList && m.exam.examnotification.Batch == m.student.EnrolledDate && m.exam.ExamType == ExamType).ToList();
            // find total and marksobtained for percentage calculation
            foreach (var result in results)
            {
                marksobtained += result.MarksObtained;
                total += result.exam.FullMarks;
            }
            ViewBag.percentage = (marksobtained/total) * 100;
            ViewBag.examtype = ExamType;
            return PartialView("_ShowResult", results );
        }


        public ActionResult ExamCreate()
        {
            System.Collections.Generic.List<SelectListItem> Examtype = new List<SelectListItem>();
            Examtype.Add(new SelectListItem { Text = "Unit Test 1", Value = "Unit Test 1" });
            Examtype.Add(new SelectListItem { Text = "First Terminal", Value = "First Terminal" });
            Examtype.Add(new SelectListItem { Text = "Unit Test 2", Value = "Unit Test 2" });
            Examtype.Add(new SelectListItem { Text = "Second Terminal", Value = "Second Terminal" });
            Examtype.Add(new SelectListItem { Text = "Unit Test 3", Value = "Unit Test 3" });
            Examtype.Add(new SelectListItem { Text = "Third Terminal", Value = "Third Terminal" });
            ViewBag.ExamType = new SelectList(Examtype, "Value", "Text", "Select one");
            return View();
        }


        [HttpPost]
        public ActionResult ExamCreate(ExamNotification model)
        {
            try
            {
                db.Entry(model).State = EntityState.Added;
                db.SaveChanges();
                Notification notice = new Notification();
                notice.Title = model.ExamType + " of all classes will be conducted on " + model.Start.Date + " onwards.";
                notice.PublishedDate = DateTime.Today.Date;
                notice.NotificationType = "Exam";
                notice.ValidUpto = model.End.Date;
                db.Entry(notice).State = EntityState.Added;
                return RedirectToAction("OverAllExam", model);
            }
            catch
            {
                return RedirectToAction("ExamCreate");
            }
        }


        public ActionResult OverAllExam(ExamNotification model)
        {
            foreach (var item in db.classinfo.ToList())
            {
                var data = db.subject.Where(m => m.ClassId == item.ClassId).ToList();
                foreach (var item1 in data)
                {
                    Exam exm = new Exam();
                    exm.ExamNoteId = model.ExamNoteId;
                    exm.ClassId = item.ClassId;
                    exm.ExamType = model.ExamType;
                    exm.SubjectId = item1.SubjectId;
                    exm.SubjectName = item1.SubjectName;
                    exm.FullMarks = 100;
                    exm.PassMarks = 40;
                    try
                    {
                        db.Entry(exm).State = EntityState.Added;
                        db.SaveChanges();
                    }
                    catch
                    {
                        return RedirectToAction("Index");
                    }

                }
            }
            return RedirectToAction("ViewExam");
        }


        //public ActionResult AddOverallMarks()
        //{
        //    var data = db.exam.Where(m => m.examnotification.Batch.Equals("2013")).ToList();
        //    foreach (var item in data)
        //    {
        //        var stds = db.student.Where(m => (m.ClassId == (item.ClassId + 1) && m.EnrolledDate.Equals("2014")) || (m.ClassId == (item.ClassId) && m.EnrolledDate.Equals("2013"))).ToList();
        //        foreach (var std in stds)
        //        {
        //            Random random = new Random();
        //            Result res = new Result();
        //            res.ExamId = item.ExamId;
        //            res.StudentId = std.StudentId;
        //            if (std.StudentMiddleName == "")
        //            {
        //                res.StudentName = std.StudentFirstName + " " + std.StudentLastName;
        //            }
        //            else
        //            {
        //                res.StudentName = std.StudentFirstName + " " + std.StudentMiddleName + " " + std.StudentLastName;
        //            }
        //            res.Rollno = std.Rollno;
        //            res.MarksObtained = random.Next(30, 97);
        //            if (res.MarksObtained < 40)
        //                res.Remarks = "Failed, Study Harder.";
        //            else if (res.MarksObtained >= 40 && res.MarksObtained <= 60)
        //                res.Remarks = "Good, But can do better";
        //            else if (res.MarksObtained > 60 && res.MarksObtained < 80)
        //                res.Remarks = "Good, Keep it up.";
        //            else
        //                res.Remarks = "Excellent, Keep it up.";
        //            try
        //            {
        //                db.Entry(res).State = EntityState.Added;
        //                db.SaveChanges();
        //            }
        //            catch
        //            {
        //                return RedirectToAction("Index", "Attendance");
        //            }

        //        }
        //    }
        //    return RedirectToAction("Index");
        //}
        //Automatically adds Marks to all created assignments (Random Marks)

        //public ActionResult AddMarksAssignment()
        //{
        //    var data = db.assignment.ToList();
        //    foreach (var item in data)
        //    {
        //        var stds = db.student.Where(m => m.ClassId == item.ClassId).ToList();
        //        foreach (var std in stds)
        //        {
        //            Random random = new Random();
        //            PerformanceIndex res = new PerformanceIndex();
        //            res.AssignmentId = item.AssignmentId;
        //            res.StudentId = std.StudentId;
        //            res.ClassId = std.ClassId;
        //            if (std.StudentMiddleName == "")
        //            {
        //                res.StudentName = std.StudentFirstName + " " + std.StudentLastName;
        //            }
        //            else
        //            {
        //                res.StudentName = std.StudentFirstName + " " + std.StudentMiddleName + " " + std.StudentLastName;
        //            }
        //            res.MarksObtained = random.Next(4, 19);
        //            if (res.MarksObtained < 4)
        //                res.Remarks = "Very poor";
        //            else if (res.MarksObtained >= 4 && res.MarksObtained <= 8)
        //                res.Remarks = "Poor, Try Harder";
        //            else if (res.MarksObtained > 8 && res.MarksObtained < 17)
        //                res.Remarks = "Good, Keep up the hardwork.";
        //            else
        //                res.Remarks = "Excellent, Keep it up.";
        //            try
        //            {
        //                db.Entry(res).State = EntityState.Added;
        //                db.SaveChanges();
        //            }
        //            catch
        //            {
        //                return RedirectToAction("Index", "Attendance");
        //            }

        //        }
        //    }
        //    return RedirectToAction("Index");
        //}


        public ActionResult ClassList()
        {
            ViewBag.selectClass = new SelectList(db.classinfo.ToList(), "ClassId", "ClassName");
            return PartialView("_ClassList");
        }


        public ActionResult GetSubjectList(int ClassId)
        {
            ViewBag.SubjectId = new SelectList(db.subject.Where(m => m.ClassId == ClassId).ToList(), "SubjectId", "SubjectName");
            return View("_GetSubjectList", ClassId);
        }


        public ActionResult ViewResult(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = db.exam.Where(m => m.ClassId == pageNumber).Count();
            ViewBag.Classlist = db.classinfo.ToList();
            if (page != null)
            {
                return PartialView("_GetExamPages", db.exam.ToList().OrderBy(m => m.ClassId).ToPagedList(pageNumber, pageSize));
            }
            else
                return View(db.exam.ToList().OrderBy(m => m.ClassId).ToPagedList(pageNumber, pageSize));
        }


        public ActionResult ViewExam()
        {
            ViewData["ClassName"] = new SelectList(db.classinfo.ToList(), "ClassId", "ClassName");
            return View(db.exam.ToList());
        }


        public ActionResult ExamPerformance(int id)
        {
            var data = db.result.Where(m => m.ExamId == id).ToList();
            if (data.Count() == 0)
            {
                Exam getexam = db.exam.Find(id);
                List<Student> getstudent = db.student.Where(m => m.ClassId == getexam.ClassId && m.EnrolledDate.Equals(batch)).ToList();
                List<Result> studentlist = new List<Result>();
                for (int i = 0; i < getstudent.Count(); i++)
                {
                    Result temp = new Result();
                    if (getstudent[i].StudentMiddleName == "")
                    {
                        temp.StudentName = getstudent[i].StudentFirstName + " " + getstudent[i].StudentLastName;
                    }
                    else
                    {
                        temp.StudentName = getstudent[i].StudentFirstName + " " + getstudent[i].StudentMiddleName + " " + getstudent[i].StudentLastName;
                    }
                    temp.StudentId = getstudent[i].StudentId;
                    temp.Rollno = getstudent[i].Rollno;
                    temp.ExamId = getexam.ExamId;
                    studentlist.Add(temp);
                }

                return View(studentlist);
            }
            else
            {

                return RedirectToAction("ExamPEdit", "Exam", new { id });
            }

        }
        [HttpPost]
        public ActionResult ExamPerformance(List<Result> examdata)
        {
            foreach (Result item in examdata)
            {
                try
                {
                    db.Entry(item).State = EntityState.Added;
                    db.SaveChanges();
                }
                catch
                {
                    return View();
                }
            }
            return RedirectToAction("MarksAdded", new { examid = examdata[0].ExamId });
        }
        public ActionResult MarksAdded(int examid)
        {
            // List<Result> resultlist = new List<Result>();
            var data = from m in db.result where (m.ExamId == examid) select m;
            return View(data);
        }
        public ActionResult ExamPEdit(int id)
        {
            //int temp;
            //temp = Int32.Parse(id);
            List<Result> data = db.result.Where(m => m.ExamId == id).ToList();
            return View(data);
        }
        [HttpPost]
        public ActionResult ExamPEdit(List<Result> resultlist)
        {
            foreach (Result item in resultlist)
            {
                try
                {
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch
                {
                    return View();
                }
            }
            return RedirectToAction("MarksAdded", new { examid = resultlist[0].ExamId });

        }
        public ActionResult GiveAssignment()
        {

            ViewBag.ClassName = new SelectList(db.classinfo.ToList(), "ClassId", "ClassName");
            return View();
        }

        //
        // POST: /Exam/ExamCreate
        [HttpPost]
        public ActionResult GiveAssignment(Assignment model)
        {
            try
            {
                model.FullMarks = 20;
                model.PassMarks = 8;
                db.Entry(model).State = EntityState.Added;
                db.SaveChanges();
                // TODO: Add insert logic here

                return RedirectToAction("AssignmentIndex", "Exam");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult AssignmentIndex()
        {
            return View(db.assignment.ToList().Take(100));
        }


        public ActionResult AddPerformance(int id)
        {
            var data = db.assignment.Find(id);
            long classid = db.assignment.Find(id).ClassId;
            List<PerformanceIndex> performancelist = new List<PerformanceIndex>();
            List<Student> listStudent = new List<Student>();
            listStudent = db.student.Where(m => m.ClassId == classid && m.EnrolledDate.Equals(batch)).ToList();
            foreach (Student item in listStudent)
            {
                PerformanceIndex list1 = new PerformanceIndex();
                list1.AssignmentId = id;
                list1.ClassId = item.ClassId;
                list1.StudentId = item.StudentId;
                list1.student = item;
                list1.assignment = data;
                if (item.StudentMiddleName == null)
                {
                    list1.StudentName = item.StudentFirstName + " " + item.StudentLastName;
                }
                else
                {
                    list1.StudentName = item.StudentFirstName + " " + item.StudentMiddleName + " " + item.StudentLastName;
                }
                performancelist.Add(list1);
            }
            return View(performancelist);
        }

        //Post
        [HttpPost]
        public ActionResult AddPerformance(List<PerformanceIndex> model)
        {

            for (int i = 0; i < model.Count(); i++)
            {
                try
                {
                    if (model[i].MarksObtained < 4)
                        model[i].Remarks = "Very poor";
                    else if (model[i].MarksObtained >= 4 && model[i].MarksObtained <= 8)
                        model[i].Remarks = "Poor, Try Harder";
                    else if (model[i].MarksObtained > 8 && model[i].MarksObtained < 17)
                        model[i].Remarks = "Good, Keep up the hardwork.";
                    else
                        model[i].Remarks = "Excellent, Keep it up.";
                    db.Entry(model[i]).State = EntityState.Added;
                    db.SaveChanges();
                    // TODO: Add insert logic here

                }
                catch
                {
                    return View("AssignmentIndex");
                }
            }
            return RedirectToAction("AssignmentIndex");



        }

        public ActionResult PrfrmIndex()
        {
            ViewData["selectAssignment"] = new SelectList(db.assignment.ToList(), "AssignmentId", "AssignemntTopic");


            return View();
        }

        // GET: /Exam/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Exam/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        //
        // GET: /Exam/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        //
        // GET: /Exam/Delete/5
        public ActionResult Delete(int id)
        {
            Assignment obj2 = db.assignment.Find(id);
            return View(obj2);
        }

        //
        // POST: /Exam/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Assignment model)
        {
            try
            {
                // TODO: Add delete logic here
                Assignment obj2 = db.assignment.Find(id);
                db.assignment.Remove(obj2);
                db.SaveChanges();
                return RedirectToAction("AssignmentIndex");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Marksheet()
        {
            return PartialView();
        }
    }
}
