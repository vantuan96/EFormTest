namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updte : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LogDetails",
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
                    Before = c.String(),
                    After = c.String(),
                })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
        }
    }
}
