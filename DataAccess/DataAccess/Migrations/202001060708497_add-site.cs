namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addsite : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "CurrentSiteId", c => c.Guid());
            AddColumn("dbo.Roles", "SiteId", c => c.Guid());
            CreateIndex("dbo.Roles", "SiteId");
            CreateIndex("dbo.Users", "CurrentSiteId");
            AddForeignKey("dbo.Roles", "SiteId", "dbo.Sites", "Id");
            AddForeignKey("dbo.Users", "CurrentSiteId", "dbo.Sites", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "CurrentSiteId", "dbo.Sites");
            DropForeignKey("dbo.Roles", "SiteId", "dbo.Sites");
            DropIndex("dbo.Users", new[] { "CurrentSiteId" });
            DropIndex("dbo.Roles", new[] { "SiteId" });
            DropColumn("dbo.Roles", "SiteId");
            DropColumn("dbo.Users", "CurrentSiteId");
        }
    }
}
