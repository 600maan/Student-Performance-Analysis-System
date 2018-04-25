using SPMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Data.Entity;

namespace SPMS.Controllers
{
    public class NotificationController : Controller
    {
        
        public UsersContext db=new UsersContext();
        static string batch = MvcApplication.GetBatch();

        public ActionResult Index(int? all)
        {
            if (all == null)
            {
                return View(db.notification.Where(m=>m.ValidUpto > DateTime.Today.Date).ToList());
            }
            else
            {
                return View(db.notification.ToList());
            }
        }


        public ActionResult NoticeCreate()
        {
            System.Collections.Generic.List<SelectListItem> NotificationType = new List<SelectListItem>();
            NotificationType.Add(new SelectListItem { Text = "Exam", Value = "Exam" });
            NotificationType.Add(new SelectListItem { Text = "Result", Value = "Result" });
            NotificationType.Add(new SelectListItem { Text = "Account", Value = "Account" });
            NotificationType.Add(new SelectListItem { Text = "Miscellaneous", Value = "Miscellaneous" });
            ViewBag.notificationtype = NotificationType;
            return View();
        }
        [HttpPost]
        public ActionResult NoticeCreate(Notification model)
        {
            db.Entry(model).State = EntityState.Added;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Meeting()
        {

            return View("Meeting");
        }

        [HttpPost]
        public ActionResult Meeting(Meeting model)
        {

            db.Entry(model).State = EntityState.Added;
            db.SaveChanges();
            return RedirectToAction("Home", "Admin");
        }


        public ActionResult ClassList()
        {
            ViewBag.selectClass = new SelectList(db.classinfo.ToList(), "ClassId", "ClassName");
            return PartialView("_ClassList");
        }
        public ActionResult GetStudentList(int classid)
        {
            ViewBag.studentList = new SelectList(db.student.Where(m => m.ClassId == classid && m.EnrolledDate.Equals(batch)).Select(m => new { studentid = m.StudentId, name = m.StudentFirstName + " " + m.StudentMiddleName + " " + m.StudentLastName }).ToList(), "studentid", "name");
            return PartialView("_GetStudentList");
        }
        public ActionResult EmailNotifications()
        {
            ViewBag.classlist = new SelectList(db.classinfo.ToList(), "ClassId", "ClassName",string.Empty);
            return View();
        }
        [HttpPost]
        public ActionResult EmailNotifications(EmailSend model,long? classid,long? studentid)
        {
            if (classid == null)
            {
                Student std = db.student.Find(studentid);
                Parent parent = db.parent.Where(m => m.StdSchoolId == std.StdSchoolId).First();
                model.EmailTo = parent.Email;
                SendMail(model);
            }
            else
            {
                var students = db.student.Where(m => m.ClassId == classid && m.EnrolledDate.Equals(batch)).ToList();
                foreach(var item in students)
                {
                    Parent parent = db.parent.Where(m => m.StdSchoolId == item.StdSchoolId).First();
                    model.EmailTo = parent.Email;
                    SendMail(model);
                }
            }
            return RedirectToAction("Index");
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
    }
}
