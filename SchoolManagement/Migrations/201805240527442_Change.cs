namespace SchoolManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Change : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Designations",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false),
                })
                .PrimaryKey(t => t.ID);
            AlterColumn("dbo.Admissions", "AdmissionDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Students", "DateOfBirth", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "DateOfBirth", c => c.DateTime());
            AlterColumn("dbo.Admissions", "AdmissionDate", c => c.DateTime());
            DropTable("dbo.Designations");
        }
    }
}
