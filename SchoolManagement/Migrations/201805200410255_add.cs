namespace SchoolManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ExamMarks", "StudentId", "dbo.Students");
            DropIndex("dbo.ExamMarks", new[] { "StudentId" });
            AddColumn("dbo.ExamMarks", "StudentClassId", c => c.Int(nullable: false));
            CreateIndex("dbo.ExamMarks", "StudentClassId");
            AddForeignKey("dbo.ExamMarks", "StudentClassId", "dbo.StudentClasses", "Id", cascadeDelete: false);
            DropColumn("dbo.ExamMarks", "StudentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ExamMarks", "StudentId", c => c.Int(nullable: false));
            DropForeignKey("dbo.ExamMarks", "StudentClassId", "dbo.StudentClasses");
            DropIndex("dbo.ExamMarks", new[] { "StudentClassId" });
            DropColumn("dbo.ExamMarks", "StudentClassId");
            CreateIndex("dbo.ExamMarks", "StudentId");
            AddForeignKey("dbo.ExamMarks", "StudentId", "dbo.Students", "Id", cascadeDelete: true);
        }
    }
}
