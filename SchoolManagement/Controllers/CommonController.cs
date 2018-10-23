using SchoolManagement.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagement.Controllers
{
    public class CommonController : Controller
    {
        private SchoolDbContext db = new SchoolDbContext();

        public JsonResult GetStudent(int session, int studentClass)
        {
            var data = db.Admission.Where(x => x.SessionId == session && x.StudentClassId == studentClass).Select(s => new
            {
                Id = s.Student.Id,
                Name = s.Student.Name
            }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRoll(int session, int studentClass)
        {
            var data = db.AssignRoll.Where(x => x.SessionId == session && x.StudentClassId == studentClass).Select(s => new
            {
                Id = s.Id,
                Roll = s.Roll + "||" + s.Student.Name
            }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCourse(int studentClass)
        {
            var classNameId = db.StudentClass.Where(x => x.Id == studentClass).Select(s => s.ClassNameId).FirstOrDefault();
            var data = db.Course.Where(x => x.ClassNameId == classNameId).Select(s => new
            {
                Id = s.Id,
                Name = s.Code + "||" + s.Name
            }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetExamTitle(int EducationLevel)
        {
            var data = db.ExamTitle.Where(x => x.EducationLevelId == EducationLevel).Select(s => new
            {
                Id = s.Id,
                Name = s.TitleName
            }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}