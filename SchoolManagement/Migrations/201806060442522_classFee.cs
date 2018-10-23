namespace SchoolManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class classFee : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClassFees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AdmissionFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ClassNameId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClassNames", t => t.ClassNameId, cascadeDelete: true)
                .Index(t => t.ClassNameId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClassFees", "ClassNameId", "dbo.ClassNames");
            DropIndex("dbo.ClassFees", new[] { "ClassNameId" });
            DropTable("dbo.ClassFees");
        }
    }
}
