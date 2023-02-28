namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_room : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "ViName", c => c.String());
            AddColumn("dbo.Rooms", "EnName", c => c.String());
            AddColumn("dbo.Rooms", "Floor", c => c.String());
            AddColumn("dbo.Rooms", "SiteId", c => c.Guid());
            CreateIndex("dbo.Rooms", "SiteId");
            AddForeignKey("dbo.Rooms", "SiteId", "dbo.Sites", "Id");
            DropColumn("dbo.Rooms", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rooms", "Name", c => c.String());
            DropForeignKey("dbo.Rooms", "SiteId", "dbo.Sites");
            DropIndex("dbo.Rooms", new[] { "SiteId" });
            DropColumn("dbo.Rooms", "SiteId");
            DropColumn("dbo.Rooms", "Floor");
            DropColumn("dbo.Rooms", "EnName");
            DropColumn("dbo.Rooms", "ViName");
        }
    }
}
