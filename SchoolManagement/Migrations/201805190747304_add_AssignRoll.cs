namespace SchoolManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_AssignRoll : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Admissions", "SectionId", "dbo.Sections");
            DropForeignKey("dbo.Admissions", "ShiftId", "dbo.Shifts");
            DropForeignKey("dbo.Admissions", "GroupId", "dbo.Groups");
            DropIndex("dbo.Admissions", new[] { "ShiftId" });
            DropIndex("dbo.Admissions", new[] { "SectionId" });
            DropIndex("dbo.Admissions", new[] { "GroupId" });
            CreateTable(
                "dbo.AssignRolls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Roll = c.String(),
                        StudentClassId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .ForeignKey("dbo.StudentClasses", t => t.StudentClassId, cascadeDelete: true)
                .Index(t => t.StudentClassId)
                .Index(t => t.StudentId);
            
            AddColumn("dbo.Admissions", "StudentClassId", c => c.Int(nullable: false));
            AlterColumn("dbo.Admissions", "GroupId", c => c.Int());
            CreateIndex("dbo.Admissions", "StudentClassId");
            CreateIndex("dbo.Admissions", "GroupId");
            AddForeignKey("dbo.Admissions", "StudentClassId", "dbo.StudentClasses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Admissions", "GroupId", "dbo.Groups", "Id");
            DropColumn("dbo.Admissions", "Roll");
            DropColumn("dbo.Admissions", "ShiftId");
            DropColumn("dbo.Admissions", "SectionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Admissions", "SectionId", c => c.Int(nullable: false));
            AddColumn("dbo.Admissions", "ShiftId", c => c.Int(nullable: false));
            AddColumn("dbo.Admissions", "Roll", c => c.String());
            DropForeignKey("dbo.Admissions", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.AssignRolls", "StudentClassId", "dbo.StudentClasses");
            DropForeignKey("dbo.AssignRolls", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Admissions", "StudentClassId", "dbo.StudentClasses");
            DropIndex("dbo.AssignRolls", new[] { "StudentId" });
            DropIndex("dbo.AssignRolls", new[] { "StudentClassId" });
            DropIndex("dbo.Admissions", new[] { "GroupId" });
            DropIndex("dbo.Admissions", new[] { "StudentClassId" });
            AlterColumn("dbo.Admissions", "GroupId", c => c.Int(nullable: false));
            DropColumn("dbo.Admissions", "StudentClassId");
            DropTable("dbo.AssignRolls");
            CreateIndex("dbo.Admissions", "GroupId");
            CreateIndex("dbo.Admissions", "SectionId");
            CreateIndex("dbo.Admissions", "ShiftId");
            AddForeignKey("dbo.Admissions", "GroupId", "dbo.Groups", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Admissions", "ShiftId", "dbo.Shifts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Admissions", "SectionId", "dbo.Sections", "Id", cascadeDelete: true);
        }
    }
}
