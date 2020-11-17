namespace TMDT_Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitData_04 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "OrderPhoneNumber", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "OrderPhoneNumber", c => c.Int(nullable: false));
        }
    }
}
