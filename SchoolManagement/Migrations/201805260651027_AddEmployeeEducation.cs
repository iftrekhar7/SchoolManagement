namespace SchoolManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmployeeEducation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EducationLevels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EducationLevelNaame = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EmployeeEducations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EducationLevelId = c.Int(nullable: false),
                        ExamTitleId = c.Int(nullable: false),
                        Major = c.String(nullable: false),
                        InstituteName = c.String(nullable: false),
                        ResultType = c.Int(nullable: false),
                        CGPA = c.Int(nullable: false),
                        Scale = c.Int(nullable: false),
                        PassingYear = c.String(nullable: false),
                        Duration = c.Int(nullable: false),
                        Achievement = c.String(),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EducationLevels", t => t.EducationLevelId, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.ExamTitles", t => t.ExamTitleId, cascadeDelete: true)
                .Index(t => t.EducationLevelId)
                .Index(t => t.ExamTitleId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.ExamTitles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TitleName = c.String(nullable: false),
                        EducationLevelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EducationLevels", t => t.EducationLevelId, cascadeDelete: false)
                .Index(t => t.EducationLevelId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmployeeEducations", "ExamTitleId", "dbo.ExamTitles");
            DropForeignKey("dbo.ExamTitles", "EducationLevelId", "dbo.EducationLevels");
            DropForeignKey("dbo.EmployeeEducations", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.EmployeeEducations", "EducationLevelId", "dbo.EducationLevels");
            DropIndex("dbo.ExamTitles", new[] { "EducationLevelId" });
            DropIndex("dbo.EmployeeEducations", new[] { "EmployeeId" });
            DropIndex("dbo.EmployeeEducations", new[] { "ExamTitleId" });
            DropIndex("dbo.EmployeeEducations", new[] { "EducationLevelId" });
            DropTable("dbo.ExamTitles");
            DropTable("dbo.EmployeeEducations");
            DropTable("dbo.EducationLevels");
        }
    }
}
