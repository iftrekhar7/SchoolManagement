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

namespace SchoolManagement.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EmployeeEducationsController : Controller
    {
        private SchoolDbContext db = new SchoolDbContext();

        // GET: EmployeeEducations
        public ActionResult Index()
        {
            var employeeEducation = db.EmployeeEducation.Include(e => e.EducationLevel).Include(e => e.Employee).Include(e => e.ExamTitle);
            return View(employeeEducation.ToList());
        }

        // GET: EmployeeEducations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeEducation employeeEducation = db.EmployeeEducation.Find(id);
            if (employeeEducation == null)
            {
                return HttpNotFound();
            }
            return View(employeeEducation);
        }

        // GET: EmployeeEducations/Create
        public ActionResult Create()
        {
            ViewBag.EducationLevelId = new SelectList(db.EducationLevel, "Id", "EducationLevelNaame");
            ViewBag.EmployeeId = new SelectList(db.Employee, "Id", "Name");
            ViewBag.ExamTitleId = new SelectList(db.ExamTitle, "Id", "TitleName");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EducationLevelId,ExamTitleId,Major,InstituteName,ResultType,CGPA,Scale,PassingYear,Duration,Achievement,EmployeeId")] EmployeeEducation employeeEducation)
        {
            if (ModelState.IsValid)
            {
                db.EmployeeEducation.Add(employeeEducation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EducationLevelId = new SelectList(db.EducationLevel, "Id", "EducationLevelNaame", employeeEducation.EducationLevelId);
            ViewBag.EmployeeId = new SelectList(db.Employee, "Id", "Name", employeeEducation.EmployeeId);
            ViewBag.ExamTitleId = new SelectList(db.ExamTitle, "Id", "TitleName", employeeEducation.ExamTitleId);
            return View(employeeEducation);
        }

        // GET: EmployeeEducations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeEducation employeeEducation = db.EmployeeEducation.Find(id);
            if (employeeEducation == null)
            {
                return HttpNotFound();
            }
            ViewBag.EducationLevelId = new SelectList(db.EducationLevel, "Id", "EducationLevelNaame", employeeEducation.EducationLevelId);
            ViewBag.EmployeeId = new SelectList(db.Employee, "Id", "Name", employeeEducation.EmployeeId);
            ViewBag.ExamTitleId = new SelectList(db.ExamTitle, "Id", "TitleName", employeeEducation.ExamTitleId);
            return View(employeeEducation);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeEducation employeeEducation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employeeEducation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Employe", new { Id = employeeEducation.EmployeeId });
            }
            ViewBag.EducationLevelId = new SelectList(db.EducationLevel, "Id", "EducationLevelNaame", employeeEducation.EducationLevelId);
            ViewBag.EmployeeId = new SelectList(db.Employee, "Id", "Name", employeeEducation.EmployeeId);
            ViewBag.ExamTitleId = new SelectList(db.ExamTitle, "Id", "TitleName", employeeEducation.ExamTitleId);
            return View(employeeEducation);
        }

        // GET: EmployeeEducations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeEducation employeeEducation = db.EmployeeEducation.Find(id);
            var emId = employeeEducation.EmployeeId;
            if (employeeEducation == null)
            {
                return HttpNotFound();
            }
             db.EmployeeEducation.Remove(employeeEducation);
             db.SaveChanges();
            return RedirectToAction("Details", "Employe", new { Id = emId });
        }

        // POST: EmployeeEducations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeeEducation employeeEducation = db.EmployeeEducation.Find(id);
            db.EmployeeEducation.Remove(employeeEducation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
