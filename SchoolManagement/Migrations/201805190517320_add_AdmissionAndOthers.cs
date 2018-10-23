namespace SchoolManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_AdmissionAndOthers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AdmissionDate = c.DateTime(),
                        PreviousSchool = c.String(),
                        PreviousSchoolAddrs = c.String(),
                        PreviousSchoolDocument = c.Binary(),
                        Extension = c.String(),
                        Roll = c.String(),
                        ShiftId = c.Int(nullable: false),
                        SessionId = c.Int(nullable: false),
                        SectionId = c.Int(nullable: false),
                        GroupId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.Sections", t => t.SectionId, cascadeDelete: true)
                .ForeignKey("dbo.Sessions", t => t.SessionId, cascadeDelete: true)
                .ForeignKey("dbo.Shifts", t => t.ShiftId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.ShiftId)
                .Index(t => t.SessionId)
                .Index(t => t.SectionId)
                .Index(t => t.GroupId)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sessions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Shifts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Admissions", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Admissions", "ShiftId", "dbo.Shifts");
            DropForeignKey("dbo.Admissions", "SessionId", "dbo.Sessions");
            DropForeignKey("dbo.Admissions", "SectionId", "dbo.Sections");
            DropForeignKey("dbo.Admissions", "GroupId", "dbo.Groups");
            DropIndex("dbo.Admissions", new[] { "StudentId" });
            DropIndex("dbo.Admissions", new[] { "GroupId" });
            DropIndex("dbo.Admissions", new[] { "SectionId" });
            DropIndex("dbo.Admissions", new[] { "SessionId" });
            DropIndex("dbo.Admissions", new[] { "ShiftId" });
            DropTable("dbo.Shifts");
            DropTable("dbo.Sessions");
            DropTable("dbo.Sections");
            DropTable("dbo.Groups");
            DropTable("dbo.Admissions");
        }
    }
}
