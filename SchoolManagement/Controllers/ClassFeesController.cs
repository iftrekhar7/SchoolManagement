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
    public class ClassFeesController : Controller
    {
        private SchoolDbContext db = new SchoolDbContext();

        // GET: ClassFees
        public ActionResult Index()
        {
            var classFee = db.ClassFee.Include(c => c.ClassName);
            return View(classFee.ToList());
        }

        // GET: ClassFees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassFee classFee = db.ClassFee.Find(id);
            if (classFee == null)
            {
                return HttpNotFound();
            }
            return View(classFee);
        }

        // GET: ClassFees/Create
        public ActionResult Create()
        {
            ViewBag.ClassNameId = new SelectList(db.ClassName, "ID", "Name");
            return View();
        }

        // POST: ClassFees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AdmissionFee,ClassNameId")] ClassFee classFee)
        {
            if (ModelState.IsValid)
            {
                db.ClassFee.Add(classFee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassNameId = new SelectList(db.ClassName, "ID", "Name", classFee.ClassNameId);
            return View(classFee);
        }

        // GET: ClassFees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassFee classFee = db.ClassFee.Find(id);
            if (classFee == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassNameId = new SelectList(db.ClassName, "ID", "Name", classFee.ClassNameId);
            return View(classFee);
        }

        // POST: ClassFees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AdmissionFee,ClassNameId")] ClassFee classFee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(classFee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassNameId = new SelectList(db.ClassName, "ID", "Name", classFee.ClassNameId);
            return View(classFee);
        }

        // GET: ClassFees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassFee classFee = db.ClassFee.Find(id);
            if (classFee == null)
            {
                return HttpNotFound();
            }
            return View(classFee);
        }

        // POST: ClassFees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClassFee classFee = db.ClassFee.Find(id);
            db.ClassFee.Remove(classFee);
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
