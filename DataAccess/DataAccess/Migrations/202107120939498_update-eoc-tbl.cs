namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateeoctbl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EOCTransfers", "SpecialtyId", c => c.Guid());
            AddColumn("dbo.EOCTransfers", "SiteId", c => c.Guid());
            AddColumn("dbo.EOCTransfers", "CustomerId", c => c.Guid());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EOCTransfers", "CustomerId");
            DropColumn("dbo.EOCTransfers", "SiteId");
            DropColumn("dbo.EOCTransfers", "SpecialtyId");
        }
    }
}
