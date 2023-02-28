namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addunlockformtoupdate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UnlockFormToUpdates",
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
                        Username = c.String(),
                        FormId = c.Guid(),
                        ExpiredAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ComplexOutpatientCaseSummaries", "VisitTypeGroupCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ComplexOutpatientCaseSummaries", "VisitTypeGroupCode");
            DropTable("dbo.UnlockFormToUpdates");
        }
    }
}
