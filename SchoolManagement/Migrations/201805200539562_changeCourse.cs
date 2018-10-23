namespace SchoolManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeCourse : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "StudentClassId", "dbo.StudentClasses");
            DropIndex("dbo.Courses", new[] { "StudentClassId" });
            AddColumn("dbo.Courses", "ClassNameId", c => c.Int(nullable: false));
            CreateIndex("dbo.Courses", "ClassNameId");
            AddForeignKey("dbo.Courses", "ClassNameId", "dbo.ClassNames", "ID", cascadeDelete: true);
            DropColumn("dbo.Courses", "StudentClassId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "StudentClassId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Courses", "ClassNameId", "dbo.ClassNames");
            DropIndex("dbo.Courses", new[] { "ClassNameId" });
            DropColumn("dbo.Courses", "ClassNameId");
            CreateIndex("dbo.Courses", "StudentClassId");
            AddForeignKey("dbo.Courses", "StudentClassId", "dbo.StudentClasses", "Id", cascadeDelete: true);
        }
    }
}
