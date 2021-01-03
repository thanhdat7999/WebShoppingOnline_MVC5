namespace TMDT_Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitData_03 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Discounts", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Discounts", "Status");
        }
    }
}
