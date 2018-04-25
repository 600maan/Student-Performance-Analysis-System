using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPMS.Models
{
    public class Accounting
    {
        [Key]
        public long AccId { get; set; }
        [DataType(DataType.Date)]

        public DateTime Date { get; set; }
        [Display(Name = "Class")]
        [Required]
        public long  ClassId { get; set; }
        [Display(Name="Roll Number")]
        public int Rollno { get; set; }
        [Display(Name = "Student School ID")]
        public int StdSchoolId { get; set; }
        public decimal TobePaid { get; set; }
        [RegularExpression(@"\A\d+(\.\d{1,2})?\Z", ErrorMessage = "Please enter a numeric value with up to two decimal places.")]

        public decimal PaidAmount { get; set; }
        [RegularExpression(@"\A\d+(\.\d{1,2})?\Z", ErrorMessage = "Please enter a numeric value with up to two decimal places.")]

        public decimal Due { get; set; }
        public decimal Balance { get; set; }
        public decimal TotalFee { get; set; }
        public virtual Student student { get; set; }




    }
}