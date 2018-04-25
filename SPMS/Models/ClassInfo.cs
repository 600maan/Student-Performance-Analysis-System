using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SPMS.Models
{
    public class ClassInfo
    {
        [Key]
        public long ClassId { get; set; }
        public string ClassName { get; set; }


       
       [DisplayName("Class Teacher")]
        
        public long TeacherId { get; set; }
        
        public virtual Teacher teacher { get; set; }
       
        
    }
}