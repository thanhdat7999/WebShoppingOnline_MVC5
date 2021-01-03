namespace TMDT_Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitData_02 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Codes", "EventID", "dbo.Discounts");
            DropForeignKey("dbo.Displays", "CodeID", "dbo.Codes");
            DropIndex("dbo.Codes", new[] { "EventID" });
            DropIndex("dbo.Displays", new[] { "CodeID" });
            DropTable("dbo.Codes");
            DropTable("dbo.Displays");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Displays",
                c => new
                    {
                        DisplayID = c.Int(nullable: false, identity: true),
                        Image = c.String(),
                        Text = c.String(),
                        CodeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DisplayID);
            
            CreateTable(
                "dbo.Codes",
                c => new
                    {
                        CodeID = c.Int(nullable: false, identity: true),
                        EventID = c.Int(nullable: false),
                        CodeRnd = c.String(),
                    })
                .PrimaryKey(t => t.CodeID);
            
            CreateIndex("dbo.Displays", "CodeID");
            CreateIndex("dbo.Codes", "EventID");
            AddForeignKey("dbo.Displays", "CodeID", "dbo.Codes", "CodeID", cascadeDelete: true);
            AddForeignKey("dbo.Codes", "EventID", "dbo.Discounts", "EventID", cascadeDelete: true);
        }
    }
}
