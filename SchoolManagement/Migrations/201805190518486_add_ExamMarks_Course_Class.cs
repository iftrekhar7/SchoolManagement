namespace SchoolManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_ExamMarks_Course_Class : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Code = c.String(nullable: false),
                        Theory = c.Int(nullable: false),
                        Mcq = c.Int(nullable: false),
                        Practical = c.Int(nullable: false),
                        Total = c.Int(nullable: false),
                        StudentClassId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StudentClasses", t => t.StudentClassId, cascadeDelete: true)
                .Index(t => t.StudentClassId);
            
            CreateTable(
                "dbo.StudentClasses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SectionId = c.Int(nullable: false),
                        ShiftId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sections", t => t.SectionId, cascadeDelete: true)
                .ForeignKey("dbo.Shifts", t => t.ShiftId, cascadeDelete: true)
                .Index(t => t.SectionId)
                .Index(t => t.ShiftId);
            
            CreateTable(
                "dbo.ExamMarks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Theory = c.Single(nullable: false),
                        Mcq = c.Single(nullable: false),
                        Practical = c.Single(nullable: false),
                        Total = c.Single(nullable: false),
                        Grade = c.String(),
                        CourseId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.CourseId)
                .Index(t => t.StudentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExamMarks", "StudentId", "dbo.Students");
            DropForeignKey("dbo.ExamMarks", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Courses", "StudentClassId", "dbo.StudentClasses");
            DropForeignKey("dbo.StudentClasses", "ShiftId", "dbo.Shifts");
            DropForeignKey("dbo.StudentClasses", "SectionId", "dbo.Sections");
            DropIndex("dbo.ExamMarks", new[] { "StudentId" });
            DropIndex("dbo.ExamMarks", new[] { "CourseId" });
            DropIndex("dbo.StudentClasses", new[] { "ShiftId" });
            DropIndex("dbo.StudentClasses", new[] { "SectionId" });
            DropIndex("dbo.Courses", new[] { "StudentClassId" });
            DropTable("dbo.ExamMarks");
            DropTable("dbo.StudentClasses");
            DropTable("dbo.Courses");
        }
    }
}
