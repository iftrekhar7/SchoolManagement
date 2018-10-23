using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SchoolManagement.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Models.Entity
{
    public class Employee
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

        public string MaritalStatus { get; set; }
        public Religion Religion { get; set; }

        public string Nationality { get; set; }

         [Display(Name = "National ID No.")]
        public string NID { get; set; }

        [Display(Name = "Present Address")]
        public string PresentAddress  { get; set; }

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

        public ICollection<EmployeeEducation> EmployeeEducation { get; set; }
        public ICollection<EmploymentHistory> EmploymentHistory { get; set; }

        public ICollection<JobInfo> JobInfo { get; set; }
    }
}