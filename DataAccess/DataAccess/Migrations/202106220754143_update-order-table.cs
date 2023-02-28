namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateordertable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Quantity", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Quantity");
        }
    }
}
