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
    public class SchoolsController : Controller
    {
        private SchoolDbContext db = new SchoolDbContext();

        // GET: Schools
        public ActionResult Index()
        {
            return View(db.School.ToList());
        }

        // GET: Schools/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            School school = db.School.Find(id);
            if (school == null)
            {
                return HttpNotFound();
            }
            return View(school);
        }

        // GET: Schools/Create
        public ActionResult Create()
        {
            var data = db.School.FirstOrDefault();
            if (data != null)
            {
                return RedirectToAction("Edit", new { id = data.Id });
            }
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(School school, HttpPostedFileBase upload)
        {
            if (upload != null && upload.ContentLength > 0)
            {
                using (var reader = new System.IO.BinaryReader(upload.InputStream))
                {
                    school.Logo = reader.ReadBytes(upload.ContentLength);
                }

            }
            if (ModelState.IsValid)
            {
                db.School.Add(school);
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = school.Id });
            }

            return View(school);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            School school = db.School.Find(id);
            if (school == null)
            {
                return HttpNotFound();
            }
            return View(school);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(School school, HttpPostedFileBase upload)
        {
            school.Logo = db.School.Where(x => x.Id == school.Id).Select(y => y.Logo).FirstOrDefault();
            //save Display image
            if (upload != null && upload.ContentLength > 0)
            {
                using (var reader = new System.IO.BinaryReader(upload.InputStream))
                {
                    school.Logo = reader.ReadBytes(upload.ContentLength);
                }

            }
            if (ModelState.IsValid)
            {
                db.Entry(school).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = school.Id });
            }
            return View(school);
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
