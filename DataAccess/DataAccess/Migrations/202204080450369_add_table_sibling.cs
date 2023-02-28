namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_table_sibling : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IPDSiblings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VisitId = c.Guid(nullable: false),
                        Age = c.String(),
                        Gender = c.Int(nullable: false),
                        Order = c.Int(nullable: false, identity: true),
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
            DropTable("dbo.IPDSiblings");
        }
    }
}
