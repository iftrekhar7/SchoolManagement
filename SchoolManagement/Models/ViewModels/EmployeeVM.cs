using SchoolManagement.Models.Entity;
using SchoolManagement.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagement.Models.ViewModels
{
    public class EmployeeVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Required!")]
        public string Name { get; set; }

        [Display(Name = "Father's Name")]
        public string FatherName { get; set; }

        [Display(Name = "Mother's Name")]
        public string MotherName { get; set; }

        public Gender Gender { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime DOB { get; set; }

        [Display(Name = "Marital Status")]
        public string MaritalStatus { get; set; }
        public Religion Religion { get; set; }

        public string Nationality { get; set; }

        [Display(Name = "National ID No.")]
        public string NID { get; set; }

        [Display(Name = "Present Address")]
        public string PresentAddress { get; set; }

        [Display(Name = "Parmanent Address")]
        public string PermanentAddress { get; set; }

        [Required(ErrorMessage = "Required!")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Required!")]
        [EmailAddress]
        public string Email { get; set; }

        public byte[] Image { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        public string Role { get; set; }

        public int EducationLevelId { get; set; }

        public EducationLevel EducationLevel { get; set; }

        public int ExamTitleId { get; set; }

        public ExamTitle ExamTitle { get; set; }


        [Required(ErrorMessage = "Required!")]
        [Display(Name = "Concentration/ Major/Group ")]
        public string Major { get; set; }

        [Required(ErrorMessage = "Required!")]
        [Display(Name = "Institute Name ")]
        public string InstituteName { get; set; }

        [Required(ErrorMessage = "Required!")]
        public ResultType ResultType { get; set; }

        public float CGPA { get; set; }

        public int Scale { get; set; }
        public float Marks { get; set; }

        [Required(ErrorMessage = "Required!")]
        [Display(Name = "Year of passing")]
        public string PassingYear { get; set; }

        public int Duration { get; set; }

        public string Achievement { get; set; }



        [Display(Name = "Company Name")]
        [Required(ErrorMessage = "Required!")]
        public string CompanyName { get; set; }

        public string CompanyLocation { get; set; }

        [Display(Name = "Designation")]
        [Required(ErrorMessage = "Required!")]
        public string Designation { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }


        public int DesignationId { get; set; }

        public Designation NewDesignation { get; set; }

        [Display(Name = "Date of Join")]
        public DateTime DOJ { get; set; }
        public decimal Salary { get; set; }

        [Display(Name = "Total Leave")]
        public int TotalLeave { get; set; }

        public byte[] Appointment { get; set; }


        public ICollection<JobInfo> JobInfo { get; set; }
        public ICollection<EmployeeEducation> EmployeeEducation { get; set; }
        public ICollection<EmploymentHistory> EmploymentHistory { get; set; }


    }
}