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
    public class ExamTitlesController : Controller
    {
        private SchoolDbContext db = new SchoolDbContext();

       
        public ActionResult Index()
        {
            var examTitle = db.ExamTitle.Include(e => e.EducationLevel);
            return View(examTitle.ToList());
        }

       
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamTitle examTitle = db.ExamTitle.Find(id);
            if (examTitle == null)
            {
                return HttpNotFound();
            }
            return View(examTitle);
        }
        public ActionResult Create()
        {
            ViewBag.EducationLevelId = new SelectList(db.EducationLevel, "Id", "EducationLevelNaame");
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ExamTitle examTitle)
        {
            if (ModelState.IsValid)
            {
                db.ExamTitle.Add(examTitle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EducationLevelId = new SelectList(db.EducationLevel, "Id", "EducationLevelNaame", examTitle.EducationLevelId);
            return View(examTitle);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamTitle examTitle = db.ExamTitle.Find(id);
            if (examTitle == null)
            {
                return HttpNotFound();
            }
            ViewBag.EducationLevelId = new SelectList(db.EducationLevel, "Id", "EducationLevelNaame", examTitle.EducationLevelId);
            return View(examTitle);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ExamTitle examTitle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(examTitle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EducationLevelId = new SelectList(db.EducationLevel, "Id", "EducationLevelNaame", examTitle.EducationLevelId);
            return View(examTitle);
        }

        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamTitle examTitle = db.ExamTitle.Find(id);
            if (examTitle == null)
            {
                return HttpNotFound();
            }
            return View(examTitle);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExamTitle examTitle = db.ExamTitle.Find(id);
            db.ExamTitle.Remove(examTitle);
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
