namespace SchoolManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmployee : DbMigration
    {
        public override void Up()
        {
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        FatherName = c.String(),
                        MotherName = c.String(),
                        Gender = c.Int(nullable: false),
                        DOB = c.DateTime(nullable: false),
                        MaritalStatus = c.String(),
                        Religion = c.Int(nullable: false),
                        Nationality = c.String(),
                        NID = c.String(),
                        PresentAddress = c.String(),
                        PermanentAddress = c.String(),
                        Phone = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Image = c.Binary(),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
            DropTable("dbo.Employees");
        }
    }
}
