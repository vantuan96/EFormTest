namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create_user : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
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
                        Username = c.String(),
                        Roles = c.String(),
                        Fullname = c.String(),
                        Email = c.String(),
                        SiteId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sites", t => t.SiteId)
                .Index(t => t.SiteId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "SiteId", "dbo.Sites");
            DropIndex("dbo.Users", new[] { "SiteId" });
            DropTable("dbo.Users");
        }
    }
}
