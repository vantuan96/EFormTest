namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateallergyweihei : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Allergy", c => c.String());
            AddColumn("dbo.Customers", "LastUpdateAllergy", c => c.DateTime());
            AddColumn("dbo.Customers", "Weight", c => c.String());
            AddColumn("dbo.Customers", "LastUpdateWeight", c => c.DateTime());
            AddColumn("dbo.Customers", "Height", c => c.String());
            AddColumn("dbo.Customers", "LastUpdateHeight", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "LastUpdateHeight");
            DropColumn("dbo.Customers", "Height");
            DropColumn("dbo.Customers", "LastUpdateWeight");
            DropColumn("dbo.Customers", "Weight");
            DropColumn("dbo.Customers", "LastUpdateAllergy");
            DropColumn("dbo.Customers", "Allergy");
        }
    }
}
