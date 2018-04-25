using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPMS.Models
{
    public class ChartData
    {
        public ChartData(string label, int value)
        {
            this.Label = label;
            this.Value = value;
        }
        public ChartData(string label,int value,int value1,int value2,int value3)
        {
            this.Label = label;
            this.Value = value;
            this.Value1 = value1;
            this.Value2 = value2;
            this.Value3 = value3;
        }
        public string Label { get; set; }
        public int Value { get; set; }
        public int Value1 { get; set; }
        public int Value2 { get; set; }
        public int Value3 { get; set; }
    }
}