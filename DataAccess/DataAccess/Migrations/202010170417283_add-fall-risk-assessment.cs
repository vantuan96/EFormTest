namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfallriskassessment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IPDFallRiskAssessmentForAdultDatas",
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
                        IPDFallRiskAssessmentForAdultId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IPDFallRiskAssessmentForAdults", t => t.IPDFallRiskAssessmentForAdultId)
                .Index(t => t.IPDFallRiskAssessmentForAdultId);
            
            CreateTable(
                "dbo.IPDFallRiskAssessmentForAdults",
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
                        Total = c.Int(nullable: false),
                        Level = c.String(),
                        Intervention = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IPDs", t => t.IPDId)
                .Index(t => t.IPDId);
            
            AddColumn("dbo.IPDPlanOfCares", "ConfirmTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IPDFallRiskAssessmentForAdultDatas", "IPDFallRiskAssessmentForAdultId", "dbo.IPDFallRiskAssessmentForAdults");
            DropForeignKey("dbo.IPDFallRiskAssessmentForAdults", "IPDId", "dbo.IPDs");
            DropIndex("dbo.IPDFallRiskAssessmentForAdults", new[] { "IPDId" });
            DropIndex("dbo.IPDFallRiskAssessmentForAdultDatas", new[] { "IPDFallRiskAssessmentForAdultId" });
            DropColumn("dbo.IPDPlanOfCares", "ConfirmTime");
            DropTable("dbo.IPDFallRiskAssessmentForAdults");
            DropTable("dbo.IPDFallRiskAssessmentForAdultDatas");
        }
    }
}
