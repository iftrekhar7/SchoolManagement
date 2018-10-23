using SchoolManagement.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagement.Models.Entity
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Required!")]
        [Display(Name = "Students's Name")]
        public string Name { get; set; }

        [Display(Name = "Father's Name")]
        public string FatherName { get; set; }

        [Display(Name = "Mother's Name")]
        public string MotherName { get; set; }

        [Required(ErrorMessage = "Required!")]
        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Present Address")]
        public string PresentAddress { get; set; }

        [Display(Name = "Parmanent Address")]
        public string ParmanentAddress { get; set; }

        public Religion Religion { get; set; }

        public byte[] Image { get; set; }
        public Gender Gender { get; set; }

        public virtual ICollection<Guardian> Guardians { get; set; }
    }
}