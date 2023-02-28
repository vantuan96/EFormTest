namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addlogtmp_tbl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LogTmps",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Username = c.String(),
                        Action = c.String(),
                        URI = c.String(maxLength: 450, unicode: false),
                        Name = c.String(),
                        Request = c.String(),
                        Response = c.String(),
                        Ip = c.String(),
                        Reason = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.URI);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.LogTmps", new[] { "URI" });
            DropTable("dbo.LogTmps");
        }
    }
}
