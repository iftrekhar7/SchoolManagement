namespace SchoolManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmploymentHistory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmploymentHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(nullable: false),
                        CompanyLocation = c.String(),
                        Designation = c.String(nullable: false),
                        From = c.DateTime(nullable: false),
                        To = c.DateTime(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmploymentHistories", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.EmploymentHistories", new[] { "EmployeeId" });
            DropTable("dbo.EmploymentHistories");
        }
    }
}
