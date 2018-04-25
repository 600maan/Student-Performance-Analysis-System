using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPMS.Models
{
    public class EmailSend
    {
        public string EmailTo { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
    
    }
}