using SchoolManagement.DAL;
using SchoolManagement.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolManagement.Helper
{
    public class Appfunction
    {
        private SchoolDbContext db = new SchoolDbContext();
       
        public string BDDate()
        {
            DateTime utcTime = DateTime.UtcNow;
            TimeZoneInfo BdZone = TimeZoneInfo.FindSystemTimeZoneById("Bangladesh Standard Time");
            DateTime localDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, BdZone);
            return localDateTime.ToShortDateString();
        }
        public DateTime BDDateTime()
        {
            DateTime utcTime = DateTime.UtcNow;
            TimeZoneInfo BdZone = TimeZoneInfo.FindSystemTimeZoneById("Bangladesh Standard Time");
            DateTime localDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, BdZone);
            return localDateTime;
        }

        public void InsertAccountGroupData(string Name)
        {
            AccountGroup acg = new AccountGroup();
            acg.Name = Name;
            db.AccountGroup.Add(acg);
            db.SaveChanges();
        }

        public void InsertAccountData(string Name, int AccountGroup)
        {
            AccountList accountlist = new AccountList();
            accountlist.AccountGroupId = AccountGroup;
            accountlist.CurrentBalance = 0;
            accountlist.Name = Name;
            accountlist.OpeningBalance = 0;
            accountlist.Date = BDDateTime();
            db.AccountList.Add(accountlist);
            db.SaveChanges();
        }

        public void UpdateAccountListBalance(int AccountId, decimal Amount)
        {
            var olddata = db.AccountList.Where(x => x.Id == AccountId).FirstOrDefault();
            AccountList account = olddata;
            account.CurrentBalance = Amount;
            db.Entry(olddata).CurrentValues.SetValues(account);
            db.SaveChanges();
        }
    }
}