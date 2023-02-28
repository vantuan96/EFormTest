namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteinvalidsession : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.InvalidSessions", new[] { "SessionId" });
            DropTable("dbo.InvalidSessions");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.InvalidSessions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                        SessionId = c.String(maxLength: 20, unicode: false),
                        Session = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.InvalidSessions", "SessionId");
        }
    }
}
