namespace TMDT_Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitData : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Displays", "CodeID", c => c.Int(nullable: false));
            CreateIndex("dbo.Displays", "CodeID");
            AddForeignKey("dbo.Displays", "CodeID", "dbo.Codes", "CodeID", cascadeDelete: true);
            DropColumn("dbo.Displays", "Code");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Displays", "Code", c => c.String());
            DropForeignKey("dbo.Displays", "CodeID", "dbo.Codes");
            DropIndex("dbo.Displays", new[] { "CodeID" });
            DropColumn("dbo.Displays", "CodeID");
        }
    }
}
