namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addipdiaspecialrequest : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IPDInitialAssessmentSpecialRequests",
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
                        Code = c.String(),
                        Group = c.String(),
                        ViName = c.String(),
                        EnName = c.String(),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IPDs", t => t.IPDId)
                .Index(t => t.IPDId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IPDInitialAssessmentSpecialRequests", "IPDId", "dbo.IPDs");
            DropIndex("dbo.IPDInitialAssessmentSpecialRequests", new[] { "IPDId" });
            DropTable("dbo.IPDInitialAssessmentSpecialRequests");
        }
    }
}
