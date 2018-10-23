namespace SchoolManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addMarks_EmployeeEducation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmployeeEducations", "Marks", c => c.Single(nullable: false));
            AlterColumn("dbo.EmployeeEducations", "CGPA", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EmployeeEducations", "CGPA", c => c.Int(nullable: false));
            DropColumn("dbo.EmployeeEducations", "Marks");
        }
    }
}
