using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPMS.Models
{
    public class Attendance
    {
        [Key]
        public long AttendanceId { get; set; }
        [DataType(DataType.Date)]
        
        public DateTime Date { get; set; }
        [Required]
        public long ClassId { get; set; }
        public long StudentId { get; set; }
        public string StudentName { get; set; }
        [Required]
        [Display(Name="Attendance Status")]
        public bool AttendanceStatus { get; set; }
        public virtual Student student { get; set; }

        public virtual ClassInfo classinfo { get; set; }

    }
    //public class AttendanceViewMode {
    //    //List<Attendance> lstatt = new List<Attendance>();
    //    //public AttendanceViewMode() { }
    //    //public AttendanceViewMode(List<Student> infos)
    //    //{
    //    //    foreach (Student info in infos)
    //    //    {
    //    //        if (info.StudentMiddleName == null)
    //    //            this.StudentName = info.StudentFirstName + " " + info.StudentLastName;
    //    //        else
    //    //            this.StudentName = info.StudentFirstName + " " + info.StudentMiddleName + " " + info.StudentLastName;
    //    //        this.ClassId = info.ClassId;


    //    //        lstatt.Add(this);
    //    //    }
    //    //}
    //    //public virtual Attendance attendace { get; set; }
    //    public long ClassId { get; set; }
    //    public string StudentName { get; set; }
    //    public bool AttendanceStatus { get; set; }
    //    public DateTime Date { get; set; }
    //}
}