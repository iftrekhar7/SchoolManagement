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
    public class EmploymentHistoriesController : Controller
    {
        private SchoolDbContext db = new SchoolDbContext();

        // GET: EmploymentHistories
        public ActionResult Index()
        {
            var employmentHistory = db.EmploymentHistory.Include(e => e.Employee);
            return View(employmentHistory.ToList());
        }

        // GET: EmploymentHistories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmploymentHistory employmentHistory = db.EmploymentHistory.Find(id);
            if (employmentHistory == null)
            {
                return HttpNotFound();
            }
            return View(employmentHistory);
        }

        // GET: EmploymentHistories/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employee, "Id", "Name");
            return View();
        }

        // POST: EmploymentHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CompanyName,CompanyLocation,Designation,From,To,EmployeeId")] EmploymentHistory employmentHistory)
        {
            if (ModelState.IsValid)
            {
                db.EmploymentHistory.Add(employmentHistory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.Employee, "Id", "Name", employmentHistory.EmployeeId);
            return View(employmentHistory);
        }

        // GET: EmploymentHistories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmploymentHistory employmentHistory = db.EmploymentHistory.Find(id);
            if (employmentHistory == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employee, "Id", "Name", employmentHistory.EmployeeId);
            return View(employmentHistory);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( EmploymentHistory employmentHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employmentHistory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Employe", new { Id = employmentHistory.EmployeeId });
            }
            ViewBag.EmployeeId = new SelectList(db.Employee, "Id", "Name", employmentHistory.EmployeeId);
            return View(employmentHistory);
        }

        // GET: EmploymentHistories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmploymentHistory employmentHistory = db.EmploymentHistory.Find(id);
            if (employmentHistory == null)
            {
                return HttpNotFound();
            }
            var emId = employmentHistory.EmployeeId;
            db.EmploymentHistory.Remove(employmentHistory);
            db.SaveChanges();
            return RedirectToAction("Details", "Employe", new { Id = emId });
        }

        // POST: EmploymentHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmploymentHistory employmentHistory = db.EmploymentHistory.Find(id);
            db.EmploymentHistory.Remove(employmentHistory);
            db.SaveChanges();
            return RedirectToAction("Details", "Employe", new { Id = employmentHistory.EmployeeId });
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
