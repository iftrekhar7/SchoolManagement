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
    public class AdmissionsController : Controller
    {
        private SchoolDbContext db = new SchoolDbContext();

        // GET: Admissions
        public ActionResult Index()
        {
            var admissions = db.Admission.Include(a => a.Student);
            return View(admissions.ToList());
        }

        // GET: Admissions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admission admission = db.Admission.Find(id);
            if (admission == null)
            {
                return HttpNotFound();
            }
            return View(admission);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admission admission = db.Admission.Find(id);
            if (admission == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentClassId = new SelectList(db.StudentClass, "Id", "Name", admission.StudentClassId);
            ViewBag.StudentId = new SelectList(db.Student, "Id", "Name", admission.StudentId);
            ViewBag.GroupId = new SelectList(db.Group, "Id", "Name", admission.GroupId);
            return View(admission);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Admission admission, HttpPostedFileBase Document)
        {
            if (ModelState.IsValid)
            {
                if (Document != null && Document.ContentLength > 0)
                {
                    using (var reader = new System.IO.BinaryReader(Document.InputStream))
                    {
                        admission.PreviousSchoolDocument = reader.ReadBytes(Document.ContentLength);
                    }

                }
                db.Entry(admission).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentClassId = new SelectList(db.StudentClass, "Id", "Name", admission.StudentClassId);
            ViewBag.StudentId = new SelectList(db.Student, "Id", "Name", admission.StudentId);
            ViewBag.GroupId = new SelectList(db.Group, "Id", "Name", admission.GroupId);
            return View(admission);
        }

        // GET: Admissions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admission admission = db.Admission.Find(id);
            if (admission == null)
            {
                return HttpNotFound();
            }
            return View(admission);
        }

        // POST: Admissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Admission admission = db.Admission.Find(id);
            db.Admission.Remove(admission);
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
