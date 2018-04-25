using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPMS.Models
{
    public class Analysis
    {
        public long AnalysisId { get; set; }
        public long ClassId { get; set; }
        public long Subject1 { get; set; }
        public int Average1 { get; set; }
        public int FailCount1 { get; set; }
        public long Subject2 { get; set; }
        public int Average2 { get; set; }
        public int FailCount2 { get; set; }
        public long Subject3 { get; set; }
        public int Average3 { get; set; }
        public int FailCount3 { get; set; }
        public long Subject4 { get; set; }
        public int Average4 { get; set; }
        public int FailCount4 { get; set; }
        public virtual ClassInfo classinfo { get; set; }
    }
}