using SchoolManagement.DAL;
using SchoolManagement.Helper;
using SchoolManagement.Models.Entity;
using SchoolManagement.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagement.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdmisionsController : Controller
    {
        private SchoolDbContext db = new SchoolDbContext();
        private Appfunction ap = new Appfunction();

        public ActionResult Create()
        {
            var studentClass = db.StudentClass.Select(c => new
            {
                Id = c.Id,
                Name = c.ClassName.Name + " || " + c.Shift.Name + " ||" + c.Section.Name
            }).OrderBy(o => o.Name).ToList();

            var classFee = db.ClassFee.Select(c => new
            {
                Id = c.Id,
                Name = c.ClassName.Name + " || " + c.AdmissionFee
            }).OrderBy(o => o.Name).ToList();

            ViewBag.SessionId = new SelectList(db.Session, "Id", "Name");
            ViewBag.GuardianTypeId = new SelectList(db.GuardianType, "Id", "Name");
            ViewBag.StudentClassId = new SelectList(studentClass, "Id", "Name");
            ViewBag.GroupId = new SelectList(db.Group, "Id", "Name");
            ViewBag.ClassFeeId = new SelectList(classFee, "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(AdmissionVM viewModel, HttpPostedFileBase StudentImage, HttpPostedFileBase Document)
        {
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    #region validation
                    //if (!ModelState.IsValid)
                    //{
                    //    var studentClass = db.StudentClass.Select(c => new
                    //    {
                    //        Id = c.Id,
                    //        Name = c.ClassName.Name + " || " + c.Shift.Name + " ||" + c.Section.Name
                    //    }).OrderBy(o => o.Name).ToList();

                    //    ViewBag.SessionId = new SelectList(db.Session, "Id", "Name");
                    //    ViewBag.GuardianTypeId = new SelectList(db.GuardianType, "Id", "Name");
                    //    ViewBag.StudentClassId = new SelectList(studentClass, "Id", "Name");
                    //    ViewBag.GroupId = new SelectList(db.Group, "Id", "Name");
                    //    return View();
                    //}
                    #endregion

                    #region Create Students

                    var student = new Student
                    {
                        Name = viewModel.StudentName,
                        FatherName = viewModel.FatherName,
                        MotherName = viewModel.MotherName,
                        DateOfBirth = viewModel.DateOfBirth,
                        Email = viewModel.StudentEmail,
                        PresentAddress = viewModel.PresentAddress,
                        ParmanentAddress = viewModel.ParmanentAddress,
                        Religion = viewModel.Religion,
                        Gender = viewModel.Gender

                    };
                    if (StudentImage != null && StudentImage.ContentLength > 0)
                    {
                        using (var reader = new System.IO.BinaryReader(StudentImage.InputStream))
                        {
                            student.Image = reader.ReadBytes(StudentImage.ContentLength);
                        }

                    }
                    db.Student.Add(student);
                    db.SaveChanges();

                    int studentId = student.Id;
                    #endregion

                    #region Create Guardian
                    if (viewModel.GuardianName != null) 
                    {
                        var guardian = new Guardian
                        {
                            Name = viewModel.GuardianName,
                            Email = viewModel.GuardianEmail,
                            Phone = viewModel.GuardianPhone,
                            NID = viewModel.NID,
                            GuardianTypeId = viewModel.GuardianTypeId,
                            StudentId = studentId
                        };

                        db.Guardian.Add(guardian);
                        db.SaveChanges();
                    }
                   
                    #endregion

                    #region Create Admission
                    if (viewModel.SessionId != 0)
                    {
                        var admission = new Admission
                    {
                        AdmissionDate = viewModel.AdmissionDate,
                        SessionId = viewModel.SessionId,
                        PreviousSchool = viewModel.PreviousSchool,
                        PreviousSchoolAddrs = viewModel.PreviousSchoolAddrs,
                        StudentClassId = viewModel.StudentClassId,
                        GroupId = viewModel.GroupId,
                        StudentId = studentId
                    };
                    if (Document != null && Document.ContentLength > 0)
                    {
                        using (var reader = new System.IO.BinaryReader(Document.InputStream))
                        {
                            admission.PreviousSchoolDocument = reader.ReadBytes(Document.ContentLength);
                        }

                    }

                    db.Admission.Add(admission);
                    db.SaveChanges();
                    }
                    
                    #endregion

                    #region update Admission Account
                    if (viewModel.ClassFeeId != 0) 
                    {
                        var amount = db.ClassFee.Where(i => i.Id == viewModel.ClassFeeId).Select(a => a.AdmissionFee).FirstOrDefault();
                        var prevBalance = db.AccountList.Where(n => n.Name == "Admission").Select(c => c.CurrentBalance).FirstOrDefault();
                        var newBalance = prevBalance + amount;
                        ap.UpdateAccountListBalance(1000, newBalance);
                    }
                   
                    #endregion

                    dbTransaction.Commit();
                    return RedirectToAction("Index", "Students");
                }
                catch (Exception ex)
                {
                    #region catch
                    string abc = ex.Message + ex.InnerException;
                    dbTransaction.Rollback();

                    var studentClass = db.StudentClass.Select(c => new
                    {
                        Id = c.Id,
                        Name = c.ClassName.Name + " || " + c.Shift.Name + " ||" + c.Section.Name
                    }).OrderBy(o => o.Name).ToList();
                    ViewBag.SessionId = new SelectList(db.Session, "Id", "Name");
                    ViewBag.GuardianTypeId = new SelectList(db.GuardianType, "Id", "Name");
                    ViewBag.StudentClassId = new SelectList(studentClass, "Id", "Name");
                    ViewBag.GroupId = new SelectList(db.Group, "Id", "Name");
                    return RedirectToAction("Create");
                    #endregion
                }
            }
        }


    }

     
}