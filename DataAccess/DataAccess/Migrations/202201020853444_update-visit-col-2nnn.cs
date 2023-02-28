namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatevisitcol2nnn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChargeItems", "CancelBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ChargeItems", "CancelBy");
        }
    }
}
