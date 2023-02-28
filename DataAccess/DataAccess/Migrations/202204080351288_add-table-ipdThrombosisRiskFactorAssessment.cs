namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtableipdThrombosisRiskFactorAssessment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IPDThrombosisRiskFactorAssessments",
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
                        StartingAssessment = c.DateTime(),
                        FinishingAssessment = c.DateTime(),
                        PaduaTotalPoint = c.Double(nullable: false),
                        IMPROVETotalPoint = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IPDs", t => t.IPDId)
                .Index(t => t.IPDId);
            
            CreateTable(
                "dbo.IPDThrombosisRiskFactorAssessmentDatas",
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
                        IPDThrombosisRiskFactorAssessmentId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IPDThrombosisRiskFactorAssessments", t => t.IPDThrombosisRiskFactorAssessmentId)
                .Index(t => t.IPDThrombosisRiskFactorAssessmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IPDThrombosisRiskFactorAssessmentDatas", "IPDThrombosisRiskFactorAssessmentId", "dbo.IPDThrombosisRiskFactorAssessments");
            DropForeignKey("dbo.IPDThrombosisRiskFactorAssessments", "IPDId", "dbo.IPDs");
            DropIndex("dbo.IPDThrombosisRiskFactorAssessmentDatas", new[] { "IPDThrombosisRiskFactorAssessmentId" });
            DropIndex("dbo.IPDThrombosisRiskFactorAssessments", new[] { "IPDId" });
            DropTable("dbo.IPDThrombosisRiskFactorAssessmentDatas");
            DropTable("dbo.IPDThrombosisRiskFactorAssessments");
        }
    }
}
