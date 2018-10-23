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
    public class GuardiansController : Controller
    {
        private SchoolDbContext db = new SchoolDbContext();


        public ActionResult Index()
        {
            var guardian = db.Guardian.Include(g => g.GuardianType).Include(g => g.Student);
            return View(guardian.ToList());
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guardian guardian = db.Guardian.Find(id);
            if (guardian == null)
            {
                return HttpNotFound();
            }
            return View(guardian);
        }


        public ActionResult Create()
        {
            ViewBag.GuardianTypeId = new SelectList(db.GuardianType, "Id", "Name");
            ViewBag.StudentId = new SelectList(db.Student, "Id", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Phone,Email,NID,GuardianTypeId,StudentId")] Guardian guardian)
        {
            if (ModelState.IsValid)
            {
                db.Guardian.Add(guardian);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GuardianTypeId = new SelectList(db.GuardianType, "Id", "Name", guardian.GuardianTypeId);
            ViewBag.StudentId = new SelectList(db.Student, "Id", "Name", guardian.StudentId);
            return View(guardian);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guardian guardian = db.Guardian.Find(id);
            if (guardian == null)
            {
                return HttpNotFound();
            }
            ViewBag.GuardianTypeId = new SelectList(db.GuardianType, "Id", "Name", guardian.GuardianTypeId);
            ViewBag.StudentId = new SelectList(db.Student, "Id", "Name", guardian.StudentId);
            return View(guardian);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Phone,Email,NID,GuardianTypeId,StudentId")] Guardian guardian)
        {
            if (ModelState.IsValid)
            {
                db.Entry(guardian).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GuardianTypeId = new SelectList(db.GuardianType, "Id", "Name", guardian.GuardianTypeId);
            ViewBag.StudentId = new SelectList(db.Student, "Id", "Name", guardian.StudentId);
            return View(guardian);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guardian guardian = db.Guardian.Find(id);
            if (guardian == null)
            {
                return HttpNotFound();
            }
            return View(guardian);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Guardian guardian = db.Guardian.Find(id);
            db.Guardian.Remove(guardian);
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
