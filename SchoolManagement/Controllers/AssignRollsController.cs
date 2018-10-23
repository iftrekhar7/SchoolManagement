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
    [Authorize(Roles = "Admin,Employee")]
    public class AssignRollsController : Controller
    {
        private SchoolDbContext db = new SchoolDbContext();

       
        public ActionResult Index()
        {
            var assignRoll = db.AssignRoll.Include(a => a.Session).Include(a => a.Student).Include(a => a.StudentClass.ClassName);
            return View(assignRoll.ToList());
        }

        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignRoll assignRoll = db.AssignRoll.Find(id);
            if (assignRoll == null)
            {
                return HttpNotFound();
            }
            return View(assignRoll);
        }

        
        public ActionResult Create()
        {
            var studentClass = db.StudentClass.Select(c => new
            {
                Id = c.Id,
                Name = c.ClassName.Name + " || " + c.Shift.Name + " ||" + c.Section.Name
            }).OrderBy(o => o.Name).ToList();

            ViewBag.SessionId = new SelectList(db.Session, "Id", "Name");
            ViewBag.StudentClassId = new SelectList(studentClass, "Id", "Name");
            ViewBag.StudentId = new SelectList(db.Student, "Id", "Name");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AssignRoll assignRoll)
        {
            if (ModelState.IsValid)
            {
                db.AssignRoll.Add(assignRoll);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var studentClass = db.StudentClass.Select(c => new
            {
                Id = c.Id,
                Name = c.ClassName.Name + " || " + c.Shift.Name + " ||" + c.Section.Name
            }).OrderBy(o => o.Name).ToList();

            ViewBag.SessionId = new SelectList(db.Session, "Id", "Name");
            ViewBag.StudentClassId = new SelectList(studentClass, "Id", "Name");
            ViewBag.StudentId = new SelectList(db.Student, "Id", "Name");
            return View(assignRoll);
        }

        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignRoll assignRoll = db.AssignRoll.Find(id);
            if (assignRoll == null)
            {
                return HttpNotFound();
            }
            var studentClass = db.StudentClass.Select(c => new
            {
                Id = c.Id,
                Name = c.ClassName.Name + " || " + c.Shift.Name + " ||" + c.Section.Name
            }).OrderBy(o => o.Name).ToList();

            ViewBag.SessionId = new SelectList(db.Session, "Id", "Name", assignRoll.SessionId);
            ViewBag.StudentClassId = new SelectList(studentClass, "Id", "Name", assignRoll.StudentClassId);
            ViewBag.StudentId = new SelectList(db.Student, "Id", "Name", assignRoll.StudentId);
            return View(assignRoll);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( AssignRoll assignRoll)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assignRoll).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var studentClass = db.StudentClass.Select(c => new
            {
                Id = c.Id,
                Name = c.ClassName.Name + " || " + c.Shift.Name + " ||" + c.Section.Name
            }).OrderBy(o => o.Name).ToList();
            ViewBag.SessionId = new SelectList(db.Session, "Id", "Name", assignRoll.SessionId);
            ViewBag.StudentClassId = new SelectList(studentClass, "Id", "Name", assignRoll.StudentClassId);
            ViewBag.StudentId = new SelectList(db.Student, "Id", "Name", assignRoll.StudentId);
            return View(assignRoll);
        }

        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignRoll assignRoll = db.AssignRoll.Find(id);
            if (assignRoll == null)
            {
                return HttpNotFound();
            }
            return View(assignRoll);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AssignRoll assignRoll = db.AssignRoll.Find(id);
            db.AssignRoll.Remove(assignRoll);
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
