using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagement.Models.Entity
{
    public class JobInfo
    {
        public int Id { get; set; }

        public int DesignationId { get; set; }
       
        public Designation Designation { get; set; }

        [Display(Name = "Date of Join")]
        public DateTime DOJ { get; set; }
        public decimal Salary { get; set; }

        [Display(Name = "Total Leave")]
        public int TotalLeave { get; set; }

        public byte[] Appointment { get; set; }
        public string AppointmentExt { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}