using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagement.Models.Entity
{
    public class EducationLevel
    {
        public int Id { get; set; }

        [Display(Name = "Education Level Name")]
        [Required(ErrorMessage = "Required!")]
        public string EducationLevelNaame { get; set; }
    }
}