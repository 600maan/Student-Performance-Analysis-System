using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SPMS.Models
{



    public class Parent
    {
        [Key]

        public long ParentId { get; set; }
        [DisplayName(" First Name")]
      [Required]
        public string ParentFirstName { get; set; }
        [Display(Name="Middle Name")]
        public string ParentMiddleName { get; set; }
        [Required]
        [Display(Name="Last Name")]
        public string ParentLastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        [DataType(DataType.EmailAddress)]
    [Required]
        public string Email { get; set; }
        public string Relation { get; set; }
        public string Occupation { get; set; }
        [DisplayName("Student's Class")]
        public string Class { get; set; }
        [Required]
        [Display(Name="Student Name")]
        public string StudentName { get; set; }

    }
}