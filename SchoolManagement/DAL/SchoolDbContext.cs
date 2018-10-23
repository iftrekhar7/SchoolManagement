using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SchoolManagement.Models.Entity;

namespace SchoolManagement.DAL
{
    public class SchoolDbContext:DbContext
    {
        public SchoolDbContext()
            : base("SchoolDb")
        {

        }
        public DbSet<GuardianType> GuardianType { get; set; }
        public DbSet<Guardian> Guardian { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Admission> Admission { get; set; }
        public DbSet<Session> Session { get; set; }
        public DbSet<Section> Section { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<Shift> Shift { get; set; }
        public DbSet<ExamMark> ExamMark { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<StudentClass> StudentClass { get; set; }
        public DbSet<ClassName> ClassName { get; set; }
        public DbSet<AssignRoll> AssignRoll { get; set; }
        public DbSet<Designation> Designation { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<EducationLevel> EducationLevel { get; set; }
        public DbSet<ExamTitle> ExamTitle { get; set; }
        public DbSet<EmployeeEducation> EmployeeEducation { get; set; }
        public DbSet<EmploymentHistory> EmploymentHistory { get; set; }
        public DbSet<JobInfo> JobInfo { get; set; }
        public DbSet<ClassFee> ClassFee { get; set; }
        public DbSet<AccountGroup> AccountGroup { get; set; }
        public DbSet<AccountList> AccountList { get; set; }
        public DbSet<DefaultSetting> DefaultSetting { get; set; }
        public DbSet<School> School { get; set; }
        //public DbSet<SMS> SMS { get; set; }
    }
}