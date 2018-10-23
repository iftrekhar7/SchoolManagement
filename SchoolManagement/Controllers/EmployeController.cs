using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SchoolManagement.DAL;
using SchoolManagement.Models.Entity;
using SchoolManagement.Models.ViewModels;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using CrystalDecisions.Shared;
using SchoolManagement.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Net.Mail;

namespace SchoolManagement.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EmployeController : Controller
    {
        private SchoolDbContext db = new SchoolDbContext();
        ApplicationDbContext dbm = new ApplicationDbContext();
        

        public ActionResult Index()
        {
            return View(db.Employee.ToList());
        }

        
        public ActionResult Details(int? id)
        {
            List<EmployeeVM> empList = new List<EmployeeVM>();
            List<EmployeeEducation> EduList = new List<EmployeeEducation>();
            List<EmploymentHistory> HistoryList = new List<EmploymentHistory>();
            List<JobInfo> jobInfo = new List<JobInfo>();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employee = db.Employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            var empEdu = db.EmployeeEducation.Where(i => i.EmployeeId == id).ToList();
            var empHist = db.EmploymentHistory.Where(i => i.EmployeeId == id).ToList();
            var job = db.JobInfo.Where(i => i.EmployeeId == id).FirstOrDefault();
            #region retrieve Education
            foreach (var data in empEdu)
            {
                var educationLavel = db.EducationLevel.Where(i => i.Id == data.EducationLevelId).FirstOrDefault();
                var title = db.ExamTitle.Where(i => i.EducationLevelId == educationLavel.Id).FirstOrDefault();

                EmployeeEducation ed = new EmployeeEducation();
               
                ed.Id = data.Id;
                ed.EducationLevelId = data.EducationLevelId;
                ed.EducationLevel = educationLavel;
                ed.ExamTitle = title;
                ed.ExamTitleId = data.ExamTitleId;
                ed.Major = data.Major;
                ed.InstituteName = data.InstituteName;
                ed.ResultType = data.ResultType;
                ed.CGPA = data.CGPA;
                ed.Scale = data.Scale;
                ed.Marks = data.Marks;
                ed.PassingYear = data.PassingYear;
                ed.Achievement = data.Achievement;
                ed.EmployeeId = data.EmployeeId;
                EduList.Add(ed);

            }
            #endregion
            #region retrieve History
            foreach (var data in empHist)
            {
                EmploymentHistory hist = new EmploymentHistory();
                hist.Id = data.Id;
                hist.CompanyName = data.CompanyName;
                hist.CompanyLocation = data.CompanyLocation;
                hist.Designation = data.Designation;
                hist.From = data.From;
                hist.To = data.To;
                hist.EmployeeId = data.EmployeeId;
                HistoryList.Add(hist);

            }
            #endregion
            #region retrieve JobInfo
            jobInfo.Add(job);
            #endregion
            #region bind in Viewmodel
            EmployeeVM vm = new EmployeeVM
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    FatherName = employee.FatherName,
                    MotherName = employee.MotherName,
                    Gender = employee.Gender,
                    DOB = employee.DOB,
                    MaritalStatus = employee.MaritalStatus,
                    Religion = employee.Religion,
                    Nationality = employee.Nationality,
                    NID = employee.NID,
                    PresentAddress = employee.PresentAddress,
                    PermanentAddress = employee.PermanentAddress,
                    Phone = employee.Phone,
                    Email = employee.Email,
                    Image = employee.Image,
                    UserName = employee.UserName,
                    EmployeeEducation = EduList,
                    EmploymentHistory = HistoryList,
                    JobInfo = jobInfo


                };
                empList.Add(vm);
            #endregion
             
            ViewBag.EducationLevelId = new SelectList(db.EducationLevel, "Id", "EducationLevelNaame");
            ViewBag.ExamTitleId = new SelectList(db.ExamTitle, "Id", "TitleName");
            return View(empList.FirstOrDefault());
        }

        public ActionResult PrintEmployee(int? id)
        {

            List<EmployeeVM> empList = new List<EmployeeVM>();
            List<EmployeeEducation> EduList = new List<EmployeeEducation>();
            List<EmploymentHistory> HistoryList = new List<EmploymentHistory>();
            List<JobInfo> jobInfo = new List<JobInfo>();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employee = db.Employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            var empEdu = db.EmployeeEducation.Where(i => i.EmployeeId == id).ToList();
            var empHist = db.EmploymentHistory.Where(i => i.EmployeeId == id).ToList();
            var job = db.JobInfo.Where(i => i.EmployeeId == id).FirstOrDefault();
            #region retrieve Education
            foreach (var data in empEdu)
            {
                var educationLavel = db.EducationLevel.Where(i => i.Id == data.EducationLevelId).FirstOrDefault();
                var title = db.ExamTitle.Where(i => i.EducationLevelId == educationLavel.Id).FirstOrDefault();

                EmployeeEducation ed = new EmployeeEducation();

                ed.Id = data.Id;
                ed.EducationLevelId = data.EducationLevelId;
                ed.EducationLevel = educationLavel;
                ed.ExamTitle = title;
                ed.ExamTitleId = data.ExamTitleId;
                ed.Major = data.Major;
                ed.InstituteName = data.InstituteName;
                ed.ResultType = data.ResultType;
                ed.CGPA = data.CGPA;
                ed.Scale = data.Scale;
                ed.Marks = data.Marks;
                ed.PassingYear = data.PassingYear;
                ed.Achievement = data.Achievement;
                ed.EmployeeId = data.EmployeeId;
                EduList.Add(ed);

            }
            #endregion
            #region retrieve History
            foreach (var data in empHist)
            {
                EmploymentHistory hist = new EmploymentHistory();
                hist.Id = data.Id;
                hist.CompanyName = data.CompanyName;
                hist.CompanyLocation = data.CompanyLocation;
                hist.Designation = data.Designation;
                hist.From = data.From;
                hist.To = data.To;
                hist.EmployeeId = data.EmployeeId;
                HistoryList.Add(hist);

            }
            #endregion
            #region retrieve JobInfo
            jobInfo.Add(job);
            #endregion
            #region bind in Viewmodel
            EmployeeVM vm = new EmployeeVM
            {
                Id = employee.Id,
                Name = employee.Name,
                FatherName = employee.FatherName,
                MotherName = employee.MotherName,
                Gender = employee.Gender,
                DOB = employee.DOB,
                MaritalStatus = employee.MaritalStatus,
                Religion = employee.Religion,
                Nationality = employee.Nationality,
                NID = employee.NID,
                PresentAddress = employee.PresentAddress,
                PermanentAddress = employee.PermanentAddress,
                Phone = employee.Phone,
                Email = employee.Email,
                Image = employee.Image,
                UserName = employee.UserName,
                EmployeeEducation = EduList,
                EmploymentHistory = HistoryList,
                JobInfo = jobInfo


            };
            empList.Add(vm);
            #endregion
            return View(empList.FirstOrDefault());
        }
        [HttpPost]
        public ActionResult CreateEducation(EmployeeVM viewModel, int id)
        {
            try
            {
                EmployeeEducation edu = new EmployeeEducation();

                edu.EducationLevelId = viewModel.EducationLevelId;
                edu.ExamTitleId = viewModel.ExamTitleId;
                edu.Major = viewModel.Major;
                edu.InstituteName = viewModel.InstituteName;
                edu.ResultType = viewModel.ResultType;
                edu.Scale = viewModel.Scale;
                edu.CGPA = viewModel.CGPA;
                edu.Marks = viewModel.Marks;
                edu.PassingYear = viewModel.PassingYear;
                edu.Duration = viewModel.Duration;
                edu.Achievement = viewModel.Achievement;
                edu.EmployeeId = id;
                db.EmployeeEducation.Add(edu);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = id });
            }
            catch (Exception ex)
            {

                //TempData["Toastr"] = Toastr.DbError(ex.Message);
            }

            return RedirectToAction("Details", new { id = id });
        }

        [HttpPost]
        public ActionResult CreateEmployment(EmployeeVM viewModel, int id)
        {
            try
            {
                EmploymentHistory hist = new EmploymentHistory();

                hist.EmployeeId = id;
                hist.CompanyName = viewModel.CompanyName;
                hist.CompanyLocation = viewModel.CompanyLocation;
                hist.Designation = viewModel.Designation;
                hist.From = viewModel.From;
                hist.To = viewModel.To;

                db.EmploymentHistory.Add(hist);
                db.SaveChanges();

                return RedirectToAction("Details", new { id = id });
            }
            catch (Exception ex)
            {

                //TempData["Toastr"] = Toastr.DbError(ex.Message);
            }

            return RedirectToAction("Details", new { id = id });
        }
        public ActionResult Create()
        {
            
            ViewBag.Role = new SelectList(dbm.Roles, "Name", "Name");
            return View();

        }

        
        [HttpPost]
        public ActionResult Create(EmployeeVM viewModel, HttpPostedFileBase EmployeeImage)
        {
            #region validation
            //if (!ModelState.IsValid)
            //{
            //    ViewBag.EducationLevelId = new SelectList(db.EducationLevel, "Id", "EducationLevelNaame");
            //    ViewBag.ExamTitleId = new SelectList(db.ExamTitle, "Id", "TitleName");
            //    ViewBag.DesignationId = new SelectList(db.Designation, "Id", "Name");
            //    return View();
            //}
            #endregion
            #region Create Employee

            var employee = new Employee
            {
                Name = viewModel.Name,
                FatherName = viewModel.FatherName,
                MotherName = viewModel.MotherName,
                Gender = viewModel.Gender,
                DOB = viewModel.DOB,
                MaritalStatus = viewModel.MaritalStatus,
                Religion = viewModel.Religion,
                Nationality = viewModel.Nationality,
                NID = viewModel.NID,
                PresentAddress = viewModel.PresentAddress,
                PermanentAddress = viewModel.PermanentAddress,
                Phone = viewModel.Phone,
                Email = viewModel.Email,
                UserName = viewModel.UserName


            };
            if (EmployeeImage != null && EmployeeImage.ContentLength > 0)
            {
                using (var reader = new System.IO.BinaryReader(EmployeeImage.InputStream))
                {
                    employee.Image = reader.ReadBytes(EmployeeImage.ContentLength);
                }

            }
            db.Employee.Add(employee);
            db.SaveChanges();
            #endregion
            #region Create Employee user
            var UserManager =  HttpContext.GetOwinContext().Get<ApplicationUserManager>();
            var roleManager = HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            var user = new ApplicationUser { UserName = viewModel.UserName, Email = viewModel.Email };
            var newUser = UserManager.Create(user, "123@Ma");
            if (newUser.Succeeded)
            {

                var role = roleManager.FindByName(viewModel.Role);
                 if (role == null)
                 {

                     role = new IdentityRole(viewModel.Role);
                     var roleresult = roleManager.Create(role);
                 }
                var rolesForUser = UserManager.GetRoles(user.Id);
                if (!rolesForUser.Contains(role.Name))
                {
                    var assignRole = UserManager.AddToRole(user.Id, role.Name);
                }
            }
            //  int employeeId = employee.Id;
        #endregion
            #region Send Email
            //if (viewModel.Email != null)
            //{
            //    using (MailMessage emailMessage = new MailMessage("", viewModel.Email))
            //    {
            //        emailMessage.Subject = "Varifiy new user";
            //        string body = "Hello" +viewModel.UserName+ ",";
            //        body += "<br /><br />Please click the following link to varified new user";
            //        body += "<br /><a href = '" + string.Format("{0}://{1}/Employe/Index", Request.Url.Scheme, Request.Url.Authority) + "'>Click here to varifying Page.</a>";
            //        body += "<br /><br />Thanks";
            //        emailMessage.Body = body;
            //        emailMessage.IsBodyHtml = true;

            //        SmtpClient MailClient = new SmtpClient();
            //        MailClient.Send(emailMessage);
            //    }
            //}
            #endregion

            return RedirectToAction("Index");
         }


        // GET: Employe/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee employee, HttpPostedFileBase EmployeeImage)
        {
            employee.Image = db.Employee.Where(x => x.Id == employee.Id).Select(y => y.Image).FirstOrDefault();
            //save Display image
            if (EmployeeImage != null && EmployeeImage.ContentLength > 0)
            {
                using (var reader = new System.IO.BinaryReader(EmployeeImage.InputStream))
                {
                    employee.Image = reader.ReadBytes(EmployeeImage.ContentLength);
                }

            }
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: Employe/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employe/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
