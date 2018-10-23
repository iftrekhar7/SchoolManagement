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
    [Authorize(Roles = "Admin, Employee")]
    public class AppAccountController : Controller
    {

        private SchoolDbContext db = new SchoolDbContext();
        public ActionResult Index()
        {
            var accountList = db.AccountList.Include(a => a.AccountGroup);
            return View(accountList.ToList());
        }


        public ActionResult Create()
        {
            ViewBag.AccountGroupId = new SelectList(db.AccountGroup, "Id", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AccountList accountList)
        {
            if (ModelState.IsValid)
            {
                db.AccountList.Add(accountList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountGroupId = new SelectList(db.AccountGroup, "Id", "Name", accountList.AccountGroupId);
            return View(accountList);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountList accountList = db.AccountList.Find(id);
            if (accountList == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountGroupId = new SelectList(db.AccountGroup, "Id", "Name", accountList.AccountGroupId);
            return View(accountList);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( AccountList accountList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accountList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountGroupId = new SelectList(db.AccountGroup, "Id", "Name", accountList.AccountGroupId);
            return View(accountList);
        }

        // GET: AppAccount/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountList accountList = db.AccountList.Find(id);
            if (accountList == null)
            {
                return HttpNotFound();
            }
            return View(accountList);
        }

        // POST: AppAccount/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccountList accountList = db.AccountList.Find(id);
            db.AccountList.Remove(accountList);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountList accountList = db.AccountList.Find(id);
            if (accountList == null)
            {
                return HttpNotFound();
            }
            return View(accountList);
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