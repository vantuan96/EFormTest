namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateusermultsite : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "SiteId", "dbo.Sites");
            DropIndex("dbo.Users", new[] { "SiteId" });
            CreateTable(
                "dbo.UserSites",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTime(),
                        DeletedBy = c.String(),
                        SiteId = c.Guid(),
                        UserId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sites", t => t.SiteId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.SiteId)
                .Index(t => t.UserId);
            
            DropColumn("dbo.Users", "SiteId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "SiteId", c => c.Guid());
            DropForeignKey("dbo.UserSites", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserSites", "SiteId", "dbo.Sites");
            DropIndex("dbo.UserSites", new[] { "UserId" });
            DropIndex("dbo.UserSites", new[] { "SiteId" });
            DropTable("dbo.UserSites");
            CreateIndex("dbo.Users", "SiteId");
            AddForeignKey("dbo.Users", "SiteId", "dbo.Sites", "Id");
        }
    }
}
