using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPMS.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Username")]
        public long ParentUserId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name= "Password")]
        public string ParentUserPassword { get; set; }
    }
}