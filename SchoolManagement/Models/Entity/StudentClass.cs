using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolManagement.Models.Entity
{
    public class StudentClass
    {
        public int Id { get; set; }

        public int ClassNameId { get; set; }
        public ClassName ClassName { get; set; }

        public int SectionId { get; set; }
        public Section Section { get; set; }

        public int ShiftId { get; set; }

        public Shift Shift { get; set; }


    }
}