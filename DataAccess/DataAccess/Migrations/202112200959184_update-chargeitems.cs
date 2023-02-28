namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatechargeitems : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChargeItems", "Date", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ChargeItems", "Date");
        }
    }
}
