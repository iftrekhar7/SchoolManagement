using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using SchoolManagement.Models;
using SchoolManagement.DAL;
using SchoolManagement.Helper;
using SchoolManagement.Models.Entity;

namespace SchoolManagement.Controllers
{
    public class InstallerController : Controller
    {

        private SchoolDbContext db = new SchoolDbContext();
        private Appfunction ap = new Appfunction();
        ApplicationDbContext dbm = new ApplicationDbContext();

        public InstallerController()
        {

        }
        public InstallerController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private ApplicationSignInManager _signInManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set { _signInManager = value; }
        }


        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string s)
        {

            // reseed accountlist table
            db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('AccountLists', RESEED, 1000)");

            // insert default settings...
            DefaultSetting ds = new DefaultSetting();
            ds.Vat = 0;
            ds.Language = "en";
            ds.From = ap.BDDateTime();
            ds.To = ap.BDDateTime();
            ds.SMSStatus = true;
            ds.SMSBalance = 0;
            db.DefaultSetting.Add(ds);
            db.SaveChanges();



            //// insert AccountGroup data
            ap.InsertAccountGroupData("Income");
            ap.InsertAccountGroupData("Expense");


            // insert Account List Data
            ap.InsertAccountData("Admission", 1);

            List<string> roleList = new List<string>
            {
                "Teacher",
                "Employee",
                "Guardian",
                "Others"
            };
            var roleManager = HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            foreach (var item in roleList)
            {
                var role = roleManager.FindByName(item);
                if (role == null)
                {

                    role = new IdentityRole(item);
                    var roleresult = roleManager.Create(role);
                }
            } 
           
            return View();
        }
    }
}