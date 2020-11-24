namespace TMDT_Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitData_07 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Others",
                c => new
                    {
                        OtherID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        StarRating = c.Int(),
                        ListFav = c.Int(),
                    })
                .PrimaryKey(t => t.OtherID)
                .ForeignKey("dbo.Accounts", t => t.UserID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.ProductID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Others", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Others", "UserID", "dbo.Accounts");
            DropIndex("dbo.Others", new[] { "ProductID" });
            DropIndex("dbo.Others", new[] { "UserID" });
            DropTable("dbo.Others");
        }
    }
}
