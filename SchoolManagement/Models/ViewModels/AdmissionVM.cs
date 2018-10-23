using SchoolManagement.Models.Entity;
using SchoolManagement.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagement.Models.ViewModels
{
    public class AdmissionVM
    {
        [Display(Name = "Admission Date")]
        public DateTime AdmissionDate { get; set; }

        [Display(Name = "Previous School Name")]
        public string PreviousSchool { get; set; }

        [Display(Name = "Previous School Address")]
        public string PreviousSchoolAddrs { get; set; }

        [Display(Name = "Previous School Document")]
        public byte[] PreviousSchoolDocument { get; set; }

        public string Extension { get; set; }

         [Required]
        public int SessionId { get; set; }

        public virtual Session Session { get; set; }

         [Required]
        public int StudentClassId { get; set; }

        public virtual StudentClass StudentClass { get; set; }


         [Required]
        public int GroupId { get; set; }

        public virtual Group Group { get; set; }

        [Required]
        public int ClassFeeId { get; set; }

        public virtual ClassFee ClassFee { get; set; }

        [Required]
        [Display(Name = "Student's Name")]
        public string StudentName { get; set; }

        [Display(Name = "Father's Name")]
        public string FatherName { get; set; }

        [Display(Name = "Mother's Name")]
        public string MotherName { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        [EmailAddress]
        [Display(Name = "Student Email")]
        public string StudentEmail { get; set; }

        [Display(Name = "Present Address")]
        public string PresentAddress { get; set; }

        [Display(Name = "Parmanent Address")]
        public string ParmanentAddress { get; set; }

        public Religion Religion { get; set; }

        public byte[] Image { get; set; }
        public Gender Gender { get; set; }


        [Display(Name = "Guardian Name")]
        [Required]
        public string GuardianName { get; set; }

        [Display(Name = "Guardian Phone")]
        [Required]
        public int GuardianPhone { get; set; }

        [Display(Name = "Guardian Email")]
        [EmailAddress]
        public string GuardianEmail { get; set; }

        [Display(Name = "National Id Card Number")]
        [Required]
        public string NID { get; set; }

        [Display(Name = "Guardian Type")]
        public int GuardianTypeId { get; set; }
        public virtual GuardianType GuardianType { get; set; }
    }
}