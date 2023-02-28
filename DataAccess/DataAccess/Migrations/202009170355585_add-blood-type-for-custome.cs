namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addbloodtypeforcustome : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "BloodTypeABO", c => c.String());
            AddColumn("dbo.Customers", "BloodTypeRH", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "BloodTypeRH");
            DropColumn("dbo.Customers", "BloodTypeABO");
        }
    }
}
