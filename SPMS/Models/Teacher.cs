using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SPMS.Models
{
    public class Teacher
    {
        [Key]
        public long TeacherId { get; set; }
        [Required]
        [Display(Name="First Name")]
        public string TeacherFirstName { get; set; }
      [Display(Name="Middle Name")]
        public string TeacherMiddleName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string TeacherLastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

         [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        public long Phone { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Qualification { get; set; }
        [DisplayName("SubjectName")]
        public long SubjectId { get; set; }
        public virtual Subject subject { get; set; }
    }
}