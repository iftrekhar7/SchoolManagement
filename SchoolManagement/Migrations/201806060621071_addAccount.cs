namespace SchoolManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAccount : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AccountLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        OpeningBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrentBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AccountGroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountGroups", t => t.AccountGroupId, cascadeDelete: true)
                .Index(t => t.AccountGroupId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AccountLists", "AccountGroupId", "dbo.AccountGroups");
            DropIndex("dbo.AccountLists", new[] { "AccountGroupId" });
            DropTable("dbo.AccountLists");
            DropTable("dbo.AccountGroups");
        }
    }
}
