namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EDVerbalOrderTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EDVerbalOrders",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Version = c.String(),
                        VisitId = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EDVerbalOrders");
        }
    }
}
