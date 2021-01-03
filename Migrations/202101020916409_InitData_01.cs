namespace TMDT_Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitData_01 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Discounts", "CodeRnd", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Discounts", "CodeRnd");
        }
    }
}
