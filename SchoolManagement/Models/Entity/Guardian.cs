using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagement.Models.Entity
{
    public class Guardian
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Phone { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string NID { get; set; }

        [Required(ErrorMessage = "Required!")]
        public int GuardianTypeId { get; set; }
        public virtual GuardianType GuardianType { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

    }
}