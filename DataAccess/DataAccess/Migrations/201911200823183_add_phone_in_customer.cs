namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_phone_in_customer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Phone", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "Phone");
        }
    }
}
