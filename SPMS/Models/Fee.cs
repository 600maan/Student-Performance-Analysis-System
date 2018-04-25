using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPMS.Models
{
    public class Fee
    {
        [Key]
        public long FeeId { get; set; }
        [Required]
        [Display(Name="Class")]
        public string ClassName { get; set; }
        [Required]
        [Display(Name=" Security Deposit")]
     //   [MaxLength(5, ErrorMessage = "Value Cannot exceed 5 digits")]        
        public decimal SecurityDeposit { get; set; }
        [Required]
        [Display(Name="Lab Charges")]
       // [MaxLength(5, ErrorMessage = "Value Cannot exceed 5 digits")]
        public decimal LaboratoryCharges { get; set; }
        [Required]
        [Display(Name="Swimming Charges")]
        //[MaxLength(5, ErrorMessage = "Value Cannot exceed 5 digits")]
        public decimal SwimmingFee { get; set; }
        [RegularExpression(@"\A\d+(\.\d{1,2})?\Z", ErrorMessage = "Please enter a numeric value with up to two decimal places.")]
        [Required]
        [Display(Name="Tution Fee(Monthly)")]
        //[MaxLength(5, ErrorMessage = "Value Cannot exceed 5 digits")]
        public decimal MonthlyTutionFee { get; set; }
        [Required]
        [Display(Name="Hostel Fee(Monthly)")]
        //[MaxLength(5, ErrorMessage = "Value Cannot exceed 5 digits")]
        public decimal MonthlyHostelFee { get; set; }
        [Required]
        [Display(Name="Transportation Charges(Monthly)")]
        //[MaxLength(5, ErrorMessage = "Value Cannot exceed 5 digits")]
        public decimal MonthlyTransportationFee { get; set; }
        [Required]
        [Display(Name="Admission Charges")]
        //[MaxLength(5, ErrorMessage = "Value Cannot exceed 5 digits")]
        [RegularExpression(@"\A\d+(\.\d{1,2})?\Z", ErrorMessage = "Please enter a numeric value with up to two decimal places.")]
        public decimal AdmissionFee { get; set; }
        [Required]
        [Display(Name="Book Charges")]
        //[MaxLength(5, ErrorMessage = "Value Cannot exceed 5 digits")]
        [RegularExpression(@"\A\d+(\.\d{1,2})?\Z", ErrorMessage = "Please enter a numeric value with up to two decimal places.")]
        public decimal BookFee { get; set; }
        [Required]
        [Display(Name="Stationary Charges")]
       // [MaxLength(5,ErrorMessage="Value Cannot exceed 5 digits")]
        [RegularExpression(@"\A\d+(\.\d{1,2})?\Z", ErrorMessage = "Please enter a numeric value with up to two decimal places.")]
        public decimal StationaryCharges { get; set; }
        


    }

   
}

