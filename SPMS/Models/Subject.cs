using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SPMS.Models
{
    public class Subject
    {
        [Key]
        public long SubjectId { get; set; }
        [DisplayName("Subject")]
        public string SubjectName { get; set; }
        [DisplayName("Class")]
        public long ClassId { get; set; }
    }
}