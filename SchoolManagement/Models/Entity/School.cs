using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagement.Models.Entity
{
    public class School
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "School Name")]
        public string Name { get; set; }

        [Display(Name = "School Address")]
        [Required]
        public string Address { get; set; }

        [Display(Name = "School Phone Primary")]
        [Required]
        public string PhonePrimary { get; set; }

        [Display(Name = "School Phone Alt")]
        public string PhoneAlt { get; set; }

        [Display(Name = "School Fax")]
        public string Fax { get; set; }

        [Display(Name = "School Email")]
        public string Email { get; set; }

        [Display(Name = "Logo")]
        public byte[] Logo { get; set; }
    }
}