namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addlogintable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LogInFails",
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
                        IPAddress = c.String(maxLength: 20, unicode: false),
                        Time = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.IPAddress);
            
            AddColumn("dbo.Users", "SessionId", c => c.String(maxLength: 20, unicode: false));
            CreateIndex("dbo.Users", "SessionId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.LogInFails", new[] { "IPAddress" });
            DropIndex("dbo.Users", new[] { "SessionId" });
            DropColumn("dbo.Users", "SessionId");
            DropTable("dbo.LogInFails");
        }
    }
}
