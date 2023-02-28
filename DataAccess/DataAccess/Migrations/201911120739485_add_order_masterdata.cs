namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_order_masterdata : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MasterDatas", "Order", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MasterDatas", "Order");
        }
    }
}
