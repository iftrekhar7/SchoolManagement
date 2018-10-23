using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SchoolManagement.Models.Entity;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Models.Entity
{
    public class EmploymentHistory
    {
        public int Id { get; set; }

        [Display(Name = "Company Name")]
        [Required(ErrorMessage = "Required!")]
        public string CompanyName { get; set; }

        public string CompanyLocation { get; set; }

        [Display(Name = "Designation")]
        [Required(ErrorMessage = "Required!")]
        public string Designation { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        
    }
}