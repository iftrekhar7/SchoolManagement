using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolManagement.Models.ViewModels
{
    public class StudentResultVM
    {
        public ICollection<ResultVM> ResultVM { get; set; }
        public ICollection<StudentInfoVM> StudentInfoVM { get; set; }
    }
}