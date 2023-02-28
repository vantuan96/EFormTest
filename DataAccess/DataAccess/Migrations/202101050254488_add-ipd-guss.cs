namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addipdguss : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IPDGuggingSwallowingScreenDatas",
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
                        Code = c.String(),
                        Value = c.String(),
                        IPDGuggingSwallowingScreenId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IPDGuggingSwallowingScreens", t => t.IPDGuggingSwallowingScreenId)
                .Index(t => t.IPDGuggingSwallowingScreenId);
            
            CreateTable(
                "dbo.IPDGuggingSwallowingScreens",
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
                        IPDId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IPDs", t => t.IPDId)
                .Index(t => t.IPDId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IPDGuggingSwallowingScreenDatas", "IPDGuggingSwallowingScreenId", "dbo.IPDGuggingSwallowingScreens");
            DropForeignKey("dbo.IPDGuggingSwallowingScreens", "IPDId", "dbo.IPDs");
            DropIndex("dbo.IPDGuggingSwallowingScreens", new[] { "IPDId" });
            DropIndex("dbo.IPDGuggingSwallowingScreenDatas", new[] { "IPDGuggingSwallowingScreenId" });
            DropTable("dbo.IPDGuggingSwallowingScreens");
            DropTable("dbo.IPDGuggingSwallowingScreenDatas");
        }
    }
}
