namespace SchoolManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeExamMark : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExamMarks", "SessionId", c => c.Int(nullable: false));
            AddColumn("dbo.ExamMarks", "AssignRollId", c => c.Int(nullable: false));
            CreateIndex("dbo.ExamMarks", "SessionId");
            CreateIndex("dbo.ExamMarks", "AssignRollId");
            AddForeignKey("dbo.ExamMarks", "AssignRollId", "dbo.AssignRolls", "Id", cascadeDelete: false);
            AddForeignKey("dbo.ExamMarks", "SessionId", "dbo.Sessions", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExamMarks", "SessionId", "dbo.Sessions");
            DropForeignKey("dbo.ExamMarks", "AssignRollId", "dbo.AssignRolls");
            DropIndex("dbo.ExamMarks", new[] { "AssignRollId" });
            DropIndex("dbo.ExamMarks", new[] { "SessionId" });
            DropColumn("dbo.ExamMarks", "AssignRollId");
            DropColumn("dbo.ExamMarks", "SessionId");
        }
    }
}
