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
    public class JobInfoesController : Controller
    {
        private SchoolDbContext db = new SchoolDbContext();

        // GET: JobInfoes
        public ActionResult Index()
        {
            var jobInfo = db.JobInfo.Include(j => j.Designation).Include(j => j.Employee);
            return View(jobInfo.ToList());
        }

        // GET: JobInfoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobInfo jobInfo = db.JobInfo.Find(id);
            if (jobInfo == null)
            {
                return HttpNotFound();
            }
            return View(jobInfo);
        }

        // GET: JobInfoes/Create
        public ActionResult Create()
        {
            ViewBag.DesignationId = new SelectList(db.Designation, "Id", "Name");
            ViewBag.EmployeeId = new SelectList(db.Employee, "Id", "Name");
            return View();
        }

        // POST: JobInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(JobInfo jobInfo)
        {
            if (ModelState.IsValid)
            {
                db.JobInfo.Add(jobInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DesignationId = new SelectList(db.Designation, "Id", "Name", jobInfo.DesignationId);
            ViewBag.EmployeeId = new SelectList(db.Employee, "Id", "Name", jobInfo.EmployeeId);
            return View(jobInfo);
        }

        // GET: JobInfoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobInfo jobInfo = db.JobInfo.Find(id);
            if (jobInfo == null)
            {
                return HttpNotFound();
            }
            ViewBag.DesignationId = new SelectList(db.Designation, "Id", "Name", jobInfo.DesignationId);
            ViewBag.EmployeeId = new SelectList(db.Employee, "Id", "Name", jobInfo.EmployeeId);
            return View(jobInfo);
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(JobInfo jobInfo, HttpPostedFileBase AppointmentImage)
        {
            if (ModelState.IsValid)
            {
                if (AppointmentImage != null && AppointmentImage.ContentLength > 0)
                {
                    using (var reader = new System.IO.BinaryReader(AppointmentImage.InputStream))
                    {
                        jobInfo.Appointment = reader.ReadBytes(AppointmentImage.ContentLength);
                    }

                }
                db.Entry(jobInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DesignationId = new SelectList(db.Designation, "Id", "Name", jobInfo.DesignationId);
            ViewBag.EmployeeId = new SelectList(db.Employee, "Id", "Name", jobInfo.EmployeeId);
            return View(jobInfo);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobInfo jobInfo = db.JobInfo.Find(id);
            if (jobInfo == null)
            {
                return HttpNotFound();
            }
            return View(jobInfo);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JobInfo jobInfo = db.JobInfo.Find(id);
            db.JobInfo.Remove(jobInfo);
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
