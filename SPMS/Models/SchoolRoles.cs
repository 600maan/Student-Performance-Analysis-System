using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPMS.Models
{
    public class SchoolRoles
    {
        [Key]
        public long SchoolRolesId { get; set; }
        [DisplayName("Roles")]
        public string RolesName { get; set; }
      //  public long UserId { get; set; }
        
           
    }
}