namespace SchoolManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDefaultSetting : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DefaultSettings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Vat = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SMSBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SMSStatus = c.Boolean(nullable: false),
                        From = c.DateTime(nullable: false),
                        To = c.DateTime(nullable: false),
                        Language = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DefaultSettings");
        }
    }
}
