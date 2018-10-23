using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolManagement.Models.Entity
{
    public class DefaultSetting
    {
        public int Id { get; set; }
        public decimal Vat { get; set; }
        public decimal SMSBalance { get; set; }
        public bool SMSStatus { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Language { get; set; }

    }
}