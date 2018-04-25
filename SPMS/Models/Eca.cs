using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPMS.Models
{
    public class Eca
    {
        [Key]
        public long EcaId { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Display(Name="Activity")]
        public string EcaName { get; set; }
        public string Description { get; set; }
        public string ParticipantName { get; set; }
        public string Winner { get; set; }
    }
}