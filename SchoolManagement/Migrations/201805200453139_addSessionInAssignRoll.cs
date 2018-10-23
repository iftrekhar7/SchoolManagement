namespace SchoolManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSessionInAssignRoll : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AssignRolls", "SessionId", c => c.Int(nullable: false));
            CreateIndex("dbo.AssignRolls", "SessionId");
            AddForeignKey("dbo.AssignRolls", "SessionId", "dbo.Sessions", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AssignRolls", "SessionId", "dbo.Sessions");
            DropIndex("dbo.AssignRolls", new[] { "SessionId" });
            DropColumn("dbo.AssignRolls", "SessionId");
        }
    }
}
