namespace TMDT_Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitData_03 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "OrderPhoneNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "OrderPhoneNumber");
        }
    }
}
