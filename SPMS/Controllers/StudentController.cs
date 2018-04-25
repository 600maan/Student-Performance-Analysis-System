using SPMS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.Net.Mail;

namespace SPMS.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        public UsersContext db = new UsersContext();
        static string batch = MvcApplication.GetBatch();
        //
        // GET: /Student/
        
        public ActionResult Index(string classlist, string searchString, int? page)
        {
            // New Code Using Datatable plugin for sorting searching and paging

            List<Student> students = db.student.ToList();
            return View(students);

            // Previous Code
            //ViewData["classlist"] = new SelectList(db.classinfo.ToList(), "ClassName", "ClassName");
            //var data = from m in db.student where(m.EnrolledDate.Equals(batch)) select m;
            //int i = 0;
            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    data = data.Where(s => s.StudentFirstName.Contains(searchString));
            //}
            //if (!String.IsNullOrEmpty(classlist))
            //{
            //    long searchid = Int64.Parse(classlist);
            //    data = data.Where(x => x.ClassId == searchid);
            //}

            //int pageSize = 10;
            //if (page == 1)
            //{
            //    i = 1;
            //}
            //int pageNumber = (page ?? 1);
            //if (i == 1 || pageNumber != 1||this.Request.RequestType=="POST")
            //{
            //    return PartialView("_PageRender", data.OrderByDescending(x => x.StudentId).ToPagedList(pageNumber, pageSize));
            //}
            //return View(data.OrderByDescending(x => x.StudentId).ToPagedList(pageNumber, pageSize));
        }


        //GET 
        public ActionResult AddStudent()
        {
            ViewData["DropdownClass"] = new SelectList(db.classinfo.ToList(), "ClassId", "ClassId");

            var idcount = (from m in db.student select m);
            RegistrationView st1 = new RegistrationView();
            if (idcount.Count() == 0)
            {
                var temp = 1000;
                st1.StdSchoolId = temp + 1;
            }
            else
            {
                var idct = (from m in db.student select m.StdSchoolId).Max();
                st1.StdSchoolId = idct + 1; ; ;
            }

            return View(st1);
        }

        [HttpPost]
        public ActionResult AddStudent(RegistrationView model)
        {
            try
            {
                Student st1 = new Student();
                Parent pt1 = new Parent();
                pt1.ParentFirstName = model.ParentFirstName;
                pt1.ParentMiddleName = model.ParentMiddleName;
                pt1.ParentLastName = model.ParentLastName;
                pt1.Phone = model.Phone;
                pt1.Email = model.Email;
                pt1.Occupation = model.Occupation;
                pt1.Relation = model.Relation;
                pt1.StdSchoolId = model.StdSchoolId;
                pt1.UserPassword = "" + model.StdSchoolId;


                st1.EnrolledDate = model.EnrolledDate;
                st1.StudentFirstName = model.StudentFirstName;
                st1.StudentMiddleName = model.StudentMiddleName;
                st1.StudentLastName = model.StudentLastName;
                st1.Street = model.Street;
                st1.City = model.City;
                st1.Country = model.Country;
                st1.DateofBirth = model.DateofBirth;
                FileUpload(model.Photo);

                st1.ImageUrl = "/Content/Images/" + model.Photo.FileName;
                st1.ClassId = model.ClassId;
                st1.Gender = model.Gender;
                st1.Rollno = model.Rollno;
                st1.StdSchoolId = model.StdSchoolId;
                st1.Attendance = 0;

                db.parent.Add(pt1);
                db.student.Add(st1);
                db.SaveChanges();
                EmailSend item = new EmailSend();
                item.EmailTo = pt1.Email;
                item.EmailSubject = "Account Created";
                item.EmailBody = "Dear Parent,<br/>Welcome to Student Performance Monitoring System.<br/>" +
                    "Your account has been created for monitoring of your student's performance.<br/>" +
                    "Account Details:<br/> Username : " + pt1.StdSchoolId + "<br/>Password : " + pt1.StdSchoolId +
                    "<br/>Visit Us : www.spms2014.com<br/>" +
                    "Mail Us at SPMS.Project@gmail.com<br/>Thank You!!!";
                SendMail(item);
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Error Occured");
            }


        }

        public void SendMail(EmailSend model)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(model.EmailTo);
            mail.From = new MailAddress("SPMS2014@gmail.com");
            mail.Subject = model.EmailSubject;
            string Body = model.EmailBody;
            mail.Body = Body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential
            ("SPMS.Project@gmail.com", "project2014");// Enter senders User name and password
            smtp.EnableSsl = true;
            smtp.Send(mail);

        }
        public ActionResult UpdateAttendance()
        {
            Random ran = new Random();
            var data = db.student.Where(m=>m.EnrolledDate.Equals(batch)).ToList();
            foreach (var item in data)
            {
                int attend = ran.Next(90, 105);
                item.Attendance = attend;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View("Index");
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
            return View(r1);
        }
        //
        // GET: /Student/Edit/5

        public ActionResult Edit(int id)
        {
            ViewData["DropdownClass"] = new SelectList(db.classinfo.ToList(), "ClassId", "ClassId");
            //Student obj1 = db.student.Find(id);
            Student st1 = new Student();
            st1 = db.student.Find(id);
            Parent pt1 = new Parent();
            pt1 = db.parent.Where(m => m.StdSchoolId == st1.StdSchoolId).First();
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
            return View(r1);
        }

        //
        // POST: /Student/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, RegistrationView model)
        {
            try
            {
                Student s = db.student.Find(id);
                Parent p = db.parent.Where(m => m.StdSchoolId == s.StdSchoolId).First();
                p.ParentFirstName = model.ParentFirstName;
                p.ParentMiddleName = model.ParentMiddleName;
                p.ParentLastName = model.ParentLastName;
                p.Phone = model.Phone;
                p.Email = model.Email;
                p.Occupation = model.Occupation;
                p.Relation = model.Relation;
                p.StdSchoolId = model.StdSchoolId;



                // TODO: Add insert logic here
                s.EnrolledDate = model.EnrolledDate;
                s.StudentFirstName = model.StudentFirstName;
                s.StudentMiddleName = model.StudentMiddleName;
                s.StudentLastName = model.StudentLastName;
                s.Street = model.Street;
                s.City = model.City;
                s.Country = model.Country;
                s.DateofBirth = model.DateofBirth;
                s.ImageUrl = model.ImageUrl;
                s.ClassId = model.ClassId;
                s.Gender = model.Gender;
                s.Rollno = model.Rollno;
                s.StdSchoolId = model.StdSchoolId;
                s.Attendance = model.Attendance;

                db.Entry(s).State = EntityState.Modified;
                db.Entry(p).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Student/Delete/5
        public void FileUpload(HttpPostedFileBase uploadFile)
        {
            if (uploadFile.ContentLength > 0)
            {
                string filePath = Path.Combine(HttpContext.Server.MapPath("../Content/Images"),
                                               Path.GetFileName(uploadFile.FileName));
                uploadFile.SaveAs(filePath);
            }

        }

        public ActionResult Delete(int id)
        {
            Student obj2 = db.student.Find(id);

            return View(obj2);
        }

        //
        // POST: /Student/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, Student model)
        {
            try
            {
                Student obj2 = db.student.Find(id);
                db.student.Remove(obj2);
                db.SaveChanges();
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}
