using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SPMS.Models
{
    public class Assignment
    {
        [Key]
        public long AssignmentId { get; set; }
        [Required]
        [Display(Name="Class")]
        public long ClassId { get; set; }
        [Required]
        [DataType(DataType.Date)]
              
        public DateTime Date { get; set; }
        [DisplayName("Subject")]
        public long SubjectId { get; set; }
        public virtual Subject subject { get; set; }
        [Required]
        public string AssignmentTopic { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime ToSubmitDate { get; set; }
        [Display(Name="Full Marks")]
        public int FullMarks { get; set; }
        [Display(Name="Pass Marks")]
        public int PassMarks { get; set; }
    }
    public class PerformanceIndex
    {
        [Key]
        public long PerformanceId { get; set; }
        [DisplayName("Assignment Topic")]
        public long AssignmentId { get; set; }
        public long ClassId { get; set; }
        public long StudentId{ get; set; }
        [Display(Name="Name")]
        public string StudentName { get; set; }
        public int MarksObtained { get; set; }
        [Required]
        public string Remarks { get; set; }
        public virtual Assignment assignment { get; set; }
        public virtual Student student { get; set; }
    }
}