namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtranferendoscopy : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EOCTransfers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CustomerId = c.Guid(),
                        FromVisitType = c.String(),
                        FromVisitId = c.Guid(),
                        ToVisitId = c.Guid(),
                        TransferBy = c.String(),
                        TransferAt = c.DateTime(),
                        AcceptBy = c.String(),
                        AcceptAt = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EOCTransfers", "CustomerId", "dbo.Customers");
            DropIndex("dbo.EOCTransfers", new[] { "CustomerId" });
            DropTable("dbo.EOCTransfers");
        }
    }
}
