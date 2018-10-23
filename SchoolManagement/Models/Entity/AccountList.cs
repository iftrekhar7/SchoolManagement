using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagement.Models.Entity
{
    public class AccountList
    {
        public int Id { get; set; }
        [Required]
       
        public string Name { get; set; }

        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Opening Balance")]
        public decimal OpeningBalance { get; set; }
        [Required]
        [Display(Name = "Current Balance")]
        public decimal CurrentBalance { get; set; }

        [Required]
        [Display(Name = "Account Group")]
        public int AccountGroupId { get; set; }
        public virtual AccountGroup AccountGroup { get; set; }
    }
}