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
    [Authorize(Roles = "Admin, Teacher")]
    public class ExamMarksController : Controller
    {
        private SchoolDbContext db = new SchoolDbContext();

        // GET: ExamMarks
        public ActionResult Index()
        {
            var examMark = db.ExamMark.Include(e => e.Session).Include(e => e.Course).Include(e => e.StudentClass.ClassName).Include(e => e.AssignRoll);
            return View(examMark.ToList());
        }

        // GET: ExamMarks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamMark examMark = db.ExamMark.Find(id);
            if (examMark == null)
            {
                return HttpNotFound();
            }
            return View(examMark);
        }

        // GET: ExamMarks/Create
        public ActionResult Create()
        {
            var studentClass = db.StudentClass.Select(c => new
            {
                Id = c.Id,
                Name = c.ClassName.Name + " || " + c.Shift.Name + " ||" + c.Section.Name
            }).OrderBy(o => o.Name).ToList();

            ViewBag.SessionId = new SelectList(db.Session, "Id", "Name");
            ViewBag.CourseId = new SelectList(db.Course, "Id", "Name");
            ViewBag.StudentClassId = new SelectList(studentClass, "Id", "Name");
            ViewBag.AssignRollId = new SelectList(db.AssignRoll, "Id", "Roll");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ExamMark examMark)
        {
            if (ModelState.IsValid)
            {
                db.ExamMark.Add(examMark);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var studentClass = db.StudentClass.Select(c => new
            {
                Id = c.Id,
                Name = c.ClassName.Name + " || " + c.Shift.Name + " ||" + c.Section.Name
            }).OrderBy(o => o.Name).ToList();

            ViewBag.SessionId = new SelectList(db.Session, "Id", "Name");
            ViewBag.CourseId = new SelectList(db.Course, "Id", "Name");
            ViewBag.StudentClassId = new SelectList(studentClass, "Id", "Name");
            ViewBag.AssignRollId = new SelectList(db.AssignRoll, "Id", "Roll");
            return View(examMark);
        }

        // GET: ExamMarks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamMark examMark = db.ExamMark.Find(id);
            if (examMark == null)
            {
                return HttpNotFound();
            }
            var studentClass = db.StudentClass.Select(c => new
            {
                Id = c.Id,
                Name = c.ClassName.Name + " || " + c.Shift.Name + " ||" + c.Section.Name
            }).OrderBy(o => o.Name).ToList();

            ViewBag.SessionId = new SelectList(db.Session, "Id", "Name");
            ViewBag.CourseId = new SelectList(db.Course, "Id", "Name");
            ViewBag.StudentClassId = new SelectList(studentClass, "Id", "Name");
            ViewBag.AssignRollId = new SelectList(db.AssignRoll, "Id", "Roll");
            return View(examMark);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ExamMark examMark)
        {
            if (ModelState.IsValid)
            {
                db.Entry(examMark).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var studentClass = db.StudentClass.Select(c => new
            {
                Id = c.Id,
                Name = c.ClassName.Name + " || " + c.Shift.Name + " ||" + c.Section.Name
            }).OrderBy(o => o.Name).ToList();

            ViewBag.SessionId = new SelectList(db.Session, "Id", "Name");
            ViewBag.CourseId = new SelectList(db.Course, "Id", "Name");
            ViewBag.StudentClassId = new SelectList(studentClass, "Id", "Name");
            ViewBag.AssignRollId = new SelectList(db.AssignRoll, "Id", "Roll");
            return View(examMark);
        }

        // GET: ExamMarks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamMark examMark = db.ExamMark.Find(id);
            if (examMark == null)
            {
                return HttpNotFound();
            }
            return View(examMark);
        }

        // POST: ExamMarks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExamMark examMark = db.ExamMark.Find(id);
            db.ExamMark.Remove(examMark);
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
