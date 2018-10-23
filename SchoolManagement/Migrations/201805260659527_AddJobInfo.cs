namespace SchoolManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddJobInfo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JobInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DesignationId = c.Int(nullable: false),
                        DOJ = c.DateTime(nullable: false),
                        Salary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalLeave = c.Int(nullable: false),
                        Appointment = c.Binary(),
                        AppointmentExt = c.String(),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Designations", t => t.DesignationId, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.DesignationId)
                .Index(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JobInfoes", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.JobInfoes", "DesignationId", "dbo.Designations");
            DropIndex("dbo.JobInfoes", new[] { "EmployeeId" });
            DropIndex("dbo.JobInfoes", new[] { "DesignationId" });
            DropTable("dbo.JobInfoes");
        }
    }
}
