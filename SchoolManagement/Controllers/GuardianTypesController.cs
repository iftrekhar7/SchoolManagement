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
    public class GuardianTypesController : Controller
    {
        private SchoolDbContext db = new SchoolDbContext();

        // GET: GuardianTypes
        public ActionResult Index()
        {
            return View(db.GuardianType.ToList());
        }

        // GET: GuardianTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GuardianType guardianType = db.GuardianType.Find(id);
            if (guardianType == null)
            {
                return HttpNotFound();
            }
            return View(guardianType);
        }

        // GET: GuardianTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GuardianTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] GuardianType guardianType)
        {
            if (ModelState.IsValid)
            {
                db.GuardianType.Add(guardianType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(guardianType);
        }

        // GET: GuardianTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GuardianType guardianType = db.GuardianType.Find(id);
            if (guardianType == null)
            {
                return HttpNotFound();
            }
            return View(guardianType);
        }

        // POST: GuardianTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] GuardianType guardianType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(guardianType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(guardianType);
        }

        // GET: GuardianTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GuardianType guardianType = db.GuardianType.Find(id);
            if (guardianType == null)
            {
                return HttpNotFound();
            }
            return View(guardianType);
        }

        // POST: GuardianTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GuardianType guardianType = db.GuardianType.Find(id);
            db.GuardianType.Remove(guardianType);
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
