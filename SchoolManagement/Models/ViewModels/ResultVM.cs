using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolManagement.Models.ViewModels
{
    public class ResultVM
    {
        public string  SubjectCode { get; set; }
        public string SubjectName { get; set; }

        public float Theory { get; set; }
        public float Mcq { get; set; }
        public float Practical { get; set; }
        public float Total { get; set; }
        public string Grade { get; set; }


    }
}