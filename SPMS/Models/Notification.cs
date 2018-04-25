using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPMS.Models
{
    public class Notification
    {
        [Key]
        public long NotificationId { get; set; }
        public string Title { get; set; }
        [Display(Name="Published Date")]
        public DateTime PublishedDate { get; set; }
        [Display(Name="Type")]
        public string NotificationType { get; set; }
        [Display(Name="Valid Upto")]
        public DateTime ValidUpto { get; set; }
    }
}