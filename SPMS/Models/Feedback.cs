using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPMS.Models
{
    public class Feedback
    {
        [Key]
        public long feedbackId { get; set; }
        [DisplayName("Name")]
        [RegularExpression("^([a-zA-Z0-9 .&'-]+)$", ErrorMessage = "Enter valid name")]
        public string senderName { get; set; }
        [DisplayName("Message")]
        [RegularExpression("^([a-zA-Z0-9 .&'-]+)$", ErrorMessage = "Enter proper message")]
        public string message { get; set; }
        [DisplayName("Feedback To")]
        public string fdbkTo { get; set; }
    }
}