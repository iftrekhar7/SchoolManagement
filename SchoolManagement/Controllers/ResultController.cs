using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SchoolManagement.DAL;
using SchoolManagement.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagement.Controllers
{
    [Authorize(Roles = "Admin, Teacher")]
    public class ResultController : Controller
    {
        private SchoolDbContext db = new SchoolDbContext();
        // GET: Result
        public ActionResult FindResult()
        {
            var studentClass = db.StudentClass.Select(c => new
            {
                Id = c.Id,
                Name = c.ClassName.Name + " || " + c.Shift.Name + " ||" + c.Section.Name
            }).OrderBy(o => o.Name).ToList();

            ViewBag.SessionId = new SelectList(db.Session, "Id", "Name");
            ViewBag.StudentClassId = new SelectList(studentClass, "Id", "Name");
            ViewBag.AssignRollId = new SelectList(db.AssignRoll, "Id", "Roll");
            return View();
        }

        [HttpPost]
        public ActionResult FindResult(int sessionId, int studentClassId, int assignRollId)
        {
            try
            {
                List<ResultVM> resultList = new List<ResultVM>();
                List<StudentInfoVM> infoList = new List<StudentInfoVM>();
                List<StudentResultVM> studentResult = new List<StudentResultVM>();
                var session = db.Session.Where(x => x.Id == sessionId).Select(x => x.Name).FirstOrDefault();
                var className = db.StudentClass.Where(x => x.Id == studentClassId).Select(x => x.ClassName.Name).FirstOrDefault();
                var roll = db.AssignRoll.Where(x => x.SessionId == sessionId && x.StudentClassId == studentClassId).Select(s => s.Roll).FirstOrDefault();

                #region retrieve & bind studentInfo
                var studentInfo = db.Admission.Where(x => x.SessionId == sessionId && x.StudentClassId == studentClassId).Select(s => new
                {
                    StudentName = s.Student.Name,
                    FatherName = s.Student.FatherName,
                    MotherName = s.Student.MotherName,
                    BirtDate = s.Student.DateOfBirth,
                    Session = session,
                    Shift = s.StudentClass.Shift.Name,
                    Section = s.StudentClass.Section.Name,
                    GroupName = s.Group.Name,
                    ClassName = className,
                    Roll = roll
                }).ToList();

                
                foreach (var item in studentInfo)
                {
                    StudentInfoVM vm = new StudentInfoVM();

                    vm.StudentName = item.StudentName;
                    vm.FatherName = item.FatherName;
                    vm.MotherName = item.MotherName;
                    vm.BirtDate = item.BirtDate;
                    vm.Session = item.Session;
                    vm.Shift = item.Shift;
                    vm.Section = item.Section;
                    vm.GroupName = item.GroupName;
                    vm.ClassName = item.ClassName;
                    vm.Roll = item.Roll;
                    infoList.Add(vm);
                }

                #endregion
                #region retrieve & bind studentResult
                var result = db.ExamMark.Where(x => x.SessionId == sessionId && x.StudentClassId == studentClassId && x.AssignRollId == assignRollId)
                                      .Select(s => new
                                      {
                                          subjectCode = s.Course.Code,
                                          subjectName = s.Course.Name,
                                          Theory = s.Theory,
                                          Mcq = s.Mcq,
                                          Practical = s.Practical,
                                          Total = s.Total,
                                          Grade = s.Grade,
                                      }).ToList();
                foreach (var item in result) 
                {
                    ResultVM vm = new ResultVM();

                    vm.SubjectCode = item.subjectCode;
                    vm.SubjectName = item.subjectName;
                    vm.Theory = item.Theory;
                    vm.Mcq = item.Mcq;
                    vm.Practical = item.Practical;
                    vm.Total = item.Total;
                    vm.Grade = item.Grade;
                    resultList.Add(vm);
                }
                #endregion

                StudentResultVM Sr = new StudentResultVM
                {
                    ResultVM = resultList,
                    StudentInfoVM = infoList
                };
                studentResult.Add(Sr);

                #region send viewBag
                var studentClass = db.StudentClass.Select(c => new
                {
                    Id = c.Id,
                    Name = c.ClassName.Name + " || " + c.Shift.Name + " ||" + c.Section.Name
                }).OrderBy(o => o.Name).ToList();

                ViewBag.SessionId = new SelectList(db.Session, "Id", "Name");
                ViewBag.StudentClassId = new SelectList(studentClass, "Id", "Name");
                ViewBag.AssignRollId = new SelectList(db.AssignRoll, "Id", "Roll");
                #endregion

                return View(studentResult);
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public ActionResult ResultPrint(int sessionId, int studentClassId, int assignRollId)
        {
            try
            {
                var session = db.Session.Where(x => x.Id == sessionId).Select(x => x.Name).FirstOrDefault();
                var className = db.StudentClass.Where(x => x.Id == studentClassId).Select(x => x.ClassName.Name).FirstOrDefault();
                var roll = db.AssignRoll.Where(x => x.SessionId == sessionId && x.StudentClassId == studentClassId).Select(s => s.Roll).FirstOrDefault();


                var studentInfo = db.Admission.Where(x => x.SessionId == sessionId && x.StudentClassId == studentClassId).Select(s => new
                                        {
                                           StudentName= s.Student.Name,
                                           FatherName = s.Student.FatherName,
                                           MotherName = s.Student.MotherName,
                                           BirtDate   = s.Student.DateOfBirth,
                                           Session    = session,
                                           Shift      = s.StudentClass.Shift.Name,
                                           Section    = s.StudentClass.Section.Name,
                                           GroupName  = s.Group.Name,
                                           ClassName  = className,
                                           Roll       = roll
                                        }).FirstOrDefault();

                var result = db.ExamMark.Where(x => x.SessionId == sessionId && x.StudentClassId == studentClassId && x.AssignRollId == assignRollId)
                                      .Select(s => new
                                        {
                                            subjectCode = s.Course.Code,
                                            subjectName = s.Course.Name,
                                            Theory = s.Theory,
                                            Mcq = s.Mcq,
                                            Practical = s.Practical,
                                            Total = s.Total,
                                            Grade = s.Grade,
                                        }).ToList();

                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/Report/StudentResult.rpt")));

                rd.DataSourceConnections.Clear();
                rd.Refresh();
                rd.SetDataSource(result);
                rd.Subreports[0].DataSourceConnections.Clear();
                rd.Subreports[0].SetDataSource(studentInfo);
                rd.SetParameterValue("Session", session);
                rd.SetParameterValue("ClassName", className);
                var stream = rd.ExportToStream(ExportFormatType.PortableDocFormat);
                var memoryStream = new MemoryStream();
                stream.CopyTo(memoryStream);
                var buffer = memoryStream.ToArray();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-length", buffer.Length.ToString());
                Response.BinaryWrite(buffer);

            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }
    }
}