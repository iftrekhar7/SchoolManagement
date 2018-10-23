namespace SchoolManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_Guardian_Student : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Guardians",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Phone = c.Int(nullable: false),
                        Email = c.String(),
                        NID = c.String(nullable: false),
                        GuardianTypeId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GuardianTypes", t => t.GuardianTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.GuardianTypeId)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.GuardianTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        FatherName = c.String(),
                        MotherName = c.String(),
                        DateOfBirth = c.DateTime(),
                        Email = c.String(),
                        PresentAddress = c.String(),
                        ParmanentAddress = c.String(),
                        Religion = c.Int(nullable: false),
                        Image = c.Binary(),
                        Gender = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Guardians", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Guardians", "GuardianTypeId", "dbo.GuardianTypes");
            DropIndex("dbo.Guardians", new[] { "StudentId" });
            DropIndex("dbo.Guardians", new[] { "GuardianTypeId" });
            DropTable("dbo.Students");
            DropTable("dbo.GuardianTypes");
            DropTable("dbo.Guardians");
        }
    }
}
