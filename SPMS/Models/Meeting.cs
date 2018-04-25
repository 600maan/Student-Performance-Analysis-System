using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPMS.Models
{
    public class Meeting
    {
        [Key]
        public long MeetId { get; set; }
        [Required]
        public string MeetTopic { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime MeetDate { get; set; }
    }
}