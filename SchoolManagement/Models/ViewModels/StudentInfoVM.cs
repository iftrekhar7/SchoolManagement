using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolManagement.Models.ViewModels
{
    public class StudentInfoVM
    {
        public string StudentName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public DateTime? BirtDate { get; set; }
        public string Session { get; set; }
        public string Shift { get; set; }
        public string Section { get; set; }
        public string GroupName { get; set; }
        public string ClassName { get; set; }
        public string Roll { get; set; }

       

    }
}