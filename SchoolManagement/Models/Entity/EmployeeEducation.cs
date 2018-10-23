using SchoolManagement.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagement.Models.Entity
{
    public class EmployeeEducation
    {
        public int Id { get; set; }

        public int EducationLevelId { get; set; }

        public EducationLevel EducationLevel { get; set; }

        public int ExamTitleId { get; set; }

        public ExamTitle ExamTitle { get; set; }


        [Required(ErrorMessage = "Required!")]
        [Display(Name = "Concentration/ Major/Group ")]
        public string Major { get; set; }

        [Required(ErrorMessage = "Required!")]
        [Display(Name = "Institute Name ")]
        public string InstituteName  { get; set; }

        [Required(ErrorMessage = "Required!")]
        public ResultType ResultType { get; set; }

        public float CGPA { get; set; }

        public int Scale { get; set; }

        public float Marks { get; set; }

        [Required(ErrorMessage = "Required!")]
        [Display(Name = "Year of passing")]
        public string PassingYear  { get; set; }

        public int Duration { get; set; }

        public string Achievement { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}