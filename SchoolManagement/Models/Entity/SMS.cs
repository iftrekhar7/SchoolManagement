using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolManagement.Models.Entity
{
    public class SMS
    {
        public int Id { get; set; }
        public string API { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime SentTime { get; set; }
        public int Key { get; set; }
        public string UserName { get; set; }
    }
}