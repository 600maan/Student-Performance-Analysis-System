using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPMS.Models
{
    public class ExamNotification
    {
        [Key]
        public long ExamNoteId { get; set; }
        [Display(Name="Exam")]
        public string ExamType { get; set; }
        public string Batch { get; set; }
        [Display(Name = "Start Date")]
        public DateTime Start { get; set; }
        [Display(Name = "End Date")]
        public DateTime End { get; set; }
        [Display(Name = "Result Date")]
        public DateTime ResultDate { get; set; }
    }
    public class Exam
    {
        [Key]
        public long ExamId { get; set; }
        [Required]
        [Display(Name="Class")]
        public long ExamNoteId { get; set; }
        public long ClassId { get; set; }
        public string ExamType { get; set; }
        public long SubjectId { get; set; }
        [Display(Name = "Subject")]
        public string SubjectName { get; set; }
        [Required]
        [Display(Name="Full Marks")]
        public int FullMarks { get; set; }
        [Display(Name="Pass Marks")]
        public int PassMarks { get; set; }
        public virtual ExamNotification examnotification { get; set; }
        public virtual ClassInfo classinfo { get; set; }
    }
    public class Result
    {
        [Key]
        public long ResultId { get; set; }
        public long ExamId { get; set; }
        public long StudentId { get; set; }
        [Display(Name="Student")]
        public string StudentName { get; set; }
        [Required]
        [Display(Name="Roll No.")]
        public int Rollno { get; set; }
        [Display(Name = "Marks Obtained")]
        public int MarksObtained { get; set; }
        public string Remarks { get; set; }
        public virtual Student student { get; set; }
        public virtual Exam exam { get; set; }
    }
}