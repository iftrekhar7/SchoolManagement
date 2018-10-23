using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagement.Models.Entity
{
    public class Course
    {
        private int _total = 0;
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }

        public int Theory { get; set; }
        public int Mcq { get; set; }
        public int Practical { get; set; }

        public int Total
        {
            get //get method for returning value
            {
                _total = this.Theory + this.Mcq + this.Practical;
                return _total;
            }
           private set // set method for storing value in name field.
            {
                _total = value;
            }
        }
        public int ClassNameId { get; set; }

        public ClassName ClassName { get; set; }
    }
}