using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagement.Models.Entity
{
    public class ClassFee
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Admission Fee")]
        public decimal AdmissionFee { get; set; }

        public int ClassNameId { get; set; }
        public ClassName ClassName { get; set; }
    }
}