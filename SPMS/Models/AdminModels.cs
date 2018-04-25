using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SPMS.Models
{


    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }
        public DbSet<UserModel> usermodel { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<LoginModel> LoginModels { get; set; }
        public DbSet<Parent> parent { get; set; }
        public DbSet<Student> student { get; set; }
        public DbSet<Teacher> teacher { get; set; }
        public DbSet<Subject> subject { get; set; }
        public DbSet<ExamNotification> examnotification { get; set; }
        public DbSet<Notification> notification { get; set; }
        public DbSet<Exam> exam { get; set; }
        public DbSet<Assignment> assignment { get; set; }
        public DbSet<Accounting> accounting { get; set; }
        public DbSet<Result> result { get; set; }
        public DbSet<PerformanceIndex> performanceindex { get; set; }
        public DbSet<SchoolRoles> schoolroles { get; set; }
        public DbSet<Attendance> attendance { get; set; }
        public DbSet<ClassInfo> classinfo { get; set; }
        public DbSet<Fee> fee { get; set; }
        public DbSet<RegisterAndAssignRoles> registerandassignroles { get; set; }
        public DbSet<Feedback> feedback { get; set; }
        public DbSet<Analysis> analysis { get; set; }
        public DbSet<Meeting> meeting { get; set; }
       
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
    }

    public class RegisterExternalLoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        //[Required]
        //[Display(Name = "Role")]
        //public string  RoleName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    
    }
    //public class RegisterAndAssignRoles
    //{

    //    public int UserId { get; set; }
    //    [Required]
    //    [Display(Name = "User name")]
    //    public string UserName { get; set; }

    //    [Required]
    //    [Display(Name = "Role")]
    //    public string RolesName{ get; set; }

    //    [Required]
    //    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    //    [DataType(DataType.Password)]
    //    [Display(Name = "Password")]
    //    public string Password { get; set; }

    //    [DataType(DataType.Password)]
    //    [Display(Name = "Confirm password")]
    //    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    //    public string ConfirmPassword { get; set; }


    //}


    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}