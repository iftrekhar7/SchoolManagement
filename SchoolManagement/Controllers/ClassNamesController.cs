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
    public class ClassNamesController : Controller
    {
        private SchoolDbContext db = new SchoolDbContext();

        // GET: ClassNames
        public ActionResult Index()
        {
            return View(db.ClassName.ToList());
        }

        // GET: ClassNames/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassName className = db.ClassName.Find(id);
            if (className == null)
            {
                return HttpNotFound();
            }
            return View(className);
        }

        // GET: ClassNames/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClassNames/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] ClassName className)
        {
            if (ModelState.IsValid)
            {
                db.ClassName.Add(className);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(className);
        }

        // GET: ClassNames/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassName className = db.ClassName.Find(id);
            if (className == null)
            {
                return HttpNotFound();
            }
            return View(className);
        }

        // POST: ClassNames/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] ClassName className)
        {
            if (ModelState.IsValid)
            {
                db.Entry(className).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(className);
        }

        // GET: ClassNames/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassName className = db.ClassName.Find(id);
            if (className == null)
            {
                return HttpNotFound();
            }
            return View(className);
        }

        // POST: ClassNames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClassName className = db.ClassName.Find(id);
            db.ClassName.Remove(className);
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
