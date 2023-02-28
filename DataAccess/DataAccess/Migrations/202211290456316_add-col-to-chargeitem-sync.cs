namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcoltochargeitemsync : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChargeItems", "SyncAt", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ChargeItems", "SyncAt");
        }
    }
}
