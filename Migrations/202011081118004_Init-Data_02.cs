namespace TMDT_Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitData_02 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "PhoneNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accounts", "PhoneNumber");
        }
    }
}
