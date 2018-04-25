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
    public class Student
    {
        [Key]
        public long StudentId { get; set; }
        [Display(Name="Batch")]
        public string EnrolledDate { get; set; }
        [Display(Name="Student School Id")]
        public long  StdSchoolId { get; set; }
        [Required]
        [Display(Name="First Name")]
        public string StudentFirstName { get; set; }
        [Display(Name="Middle Name")]
        public string StudentMiddleName { get; set; }
        [Required]
        [Display(Name="Last Name")]
        public string StudentLastName { get; set; }
        public bool Gender { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        [Display(Name = "Class")]
        public long ClassId { get; set; }
        [Required]
        public int Rollno { get; set; }
        public string ImageUrl { get; set; }
        [NotMapped]
        public HttpPostedFileBase Photo { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateofBirth { get; set; }
        public int Attendance { get; set; }
    }
    

    public class Parent
    {
        [Key]
        public long ParentId { get; set; }
        [Required]
        public long StdSchoolId { get; set; }
        [DisplayName(" First Name")]
        [Required]
        public string ParentFirstName { get; set; }
        [Display(Name = "Middle Name")]
        public string ParentMiddleName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string ParentLastName { get; set; }
        public string Phone { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string UserPassword { get; set; }
        public string Relation { get; set; }
        public string Occupation { get; set; }
        
    }
    public class RegistrationView
    {
        [Key]
        public int Id { get; set; }
        public int Attendance { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string UserPassword { get; set; }
        [Display(Name = "Batch")]
        public string EnrolledDate { get; set; }
        [Display(Name = "Student School Id")]
        public long StdSchoolId { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string StudentFirstName { get; set; }
        [Display(Name = "Middle Name")]
        public string StudentMiddleName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string StudentLastName { get; set; }
        public bool Gender { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        [Display(Name = "Class")]
        public long ClassId { get; set; }
        [Required]
        public int Rollno { get; set; }
        public string ImageUrl { get; set; }
        [NotMapped]
        public HttpPostedFileBase Photo { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateofBirth { get; set; }
//for parent
        [DisplayName(" First Name")]
        [Required]
        public string ParentFirstName { get; set; }
        [Display(Name = "Middle Name")]
        public string ParentMiddleName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string ParentLastName { get; set; }
        public string Phone { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }
        public string Relation { get; set; }
        public string Occupation { get; set; }
    }
}