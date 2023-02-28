namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmorerelationshipinformation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "RelationshipAddress", c => c.String());
            AddColumn("dbo.Customers", "RelationshipKind", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "RelationshipKind");
            DropColumn("dbo.Customers", "RelationshipAddress");
        }
    }
}
