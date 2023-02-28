namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatestandingordermsterdata : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StandingOrderMasterDatas",
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
                        Code = c.String(),
                        Name = c.String(),
                        Drug = c.String(),
                        Dosage = c.String(),
                        Route = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.StandingOrderMasterDatas");
        }
    }
}
