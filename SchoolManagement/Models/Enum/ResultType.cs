using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagement.Models.Enum
{
    public enum ResultType
        {
            Grade = 1,

            [Display(Name = "First Division")]
            FirstDivision = 2,

            [Display(Name = "Second Division")]
            SecondDivision = 3,

            [Display(Name = "Third Division")]
            ThirdDivision = 4,

            Appeared = 5,

            Enrolled = 6,

            Awarded = 7,

            Pass = 8,

            [Display(Name = "Do Not Mention")]
            DoNotMention = 9
        }
}