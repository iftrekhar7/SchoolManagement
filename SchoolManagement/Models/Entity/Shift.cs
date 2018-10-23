using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagement.Models.Entity
{
    public class Shift
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}