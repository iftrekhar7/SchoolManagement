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
    public class StudentsController : Controller
    {
        private SchoolDbContext db = new SchoolDbContext();

        
        public ActionResult Index()
        {
            return View(db.Student.ToList());
        }

        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Student.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Student.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Student student, HttpPostedFileBase StudentImage)
        {
            if (ModelState.IsValid)
            {
                 if (StudentImage != null && StudentImage.ContentLength > 0)
                {
                    using (var reader = new System.IO.BinaryReader(StudentImage.InputStream))
                    {
                        student.Image = reader.ReadBytes(StudentImage.ContentLength);
                    }

                }
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Student.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Admission admsn = db.Admission.Where(x => x.StudentId == id).SingleOrDefault();
            if( admsn!= null)
            {
                db.Admission.Remove(admsn);
                db.SaveChanges();   
            }
            
            Guardian grdn = db.Guardian.Where(x => x.StudentId == id).SingleOrDefault();
            if( grdn!= null)
            {
                db.Guardian.Remove(grdn);
                db.SaveChanges();
            }
            
            int arId = db.AssignRoll.Where(x => x.StudentId == id).Select(x => x.Id).SingleOrDefault();
            ExamMark em = db.ExamMark.Where(x => x.AssignRollId == arId).SingleOrDefault();
            if( em!= null)
            {
                db.ExamMark.Remove(em);
                db.SaveChanges();
            }
            

            AssignRoll ar = db.AssignRoll.Where(x => x.StudentId == id).SingleOrDefault();
            if (ar != null)
            {
                db.AssignRoll.Remove(ar);
               db.SaveChanges();
            }

            Student student = db.Student.Find(id);
            db.Student.Remove(student);
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
