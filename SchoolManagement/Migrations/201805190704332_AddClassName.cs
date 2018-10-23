namespace SchoolManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClassName : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClassNames",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.StudentClasses", "ClassNameId", c => c.Int(nullable: false));
            CreateIndex("dbo.StudentClasses", "ClassNameId");
            AddForeignKey("dbo.StudentClasses", "ClassNameId", "dbo.ClassNames", "ID", cascadeDelete: true);
            DropColumn("dbo.StudentClasses", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StudentClasses", "Name", c => c.String());
            DropForeignKey("dbo.StudentClasses", "ClassNameId", "dbo.ClassNames");
            DropIndex("dbo.StudentClasses", new[] { "ClassNameId" });
            DropColumn("dbo.StudentClasses", "ClassNameId");
            DropTable("dbo.ClassNames");
        }
    }
}
