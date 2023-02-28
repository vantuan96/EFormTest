namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatecustomerinfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "IsAllergy", c => c.Boolean(nullable: false));
            AddColumn("dbo.Customers", "Relationship", c => c.String());
            AddColumn("dbo.Customers", "RelationshipContact", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "RelationshipContact");
            DropColumn("dbo.Customers", "Relationship");
            DropColumn("dbo.Customers", "IsAllergy");
        }
    }
}
