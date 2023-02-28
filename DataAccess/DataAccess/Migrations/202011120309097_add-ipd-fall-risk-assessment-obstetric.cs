namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addipdfallriskassessmentobstetric : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IPDFallRiskAssessmentForObstetricDatas",
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
                        IPDFallRiskAssessmentForObstetricId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IPDFallRiskAssessmentForObstetrics", t => t.IPDFallRiskAssessmentForObstetricId)
                .Index(t => t.IPDFallRiskAssessmentForObstetricId);
            
            CreateTable(
                "dbo.IPDFallRiskAssessmentForObstetrics",
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IPDFallRiskAssessmentForObstetricDatas", "IPDFallRiskAssessmentForObstetricId", "dbo.IPDFallRiskAssessmentForObstetrics");
            DropForeignKey("dbo.IPDFallRiskAssessmentForObstetrics", "IPDId", "dbo.IPDs");
            DropIndex("dbo.IPDFallRiskAssessmentForObstetrics", new[] { "IPDId" });
            DropIndex("dbo.IPDFallRiskAssessmentForObstetricDatas", new[] { "IPDFallRiskAssessmentForObstetricId" });
            DropTable("dbo.IPDFallRiskAssessmentForObstetrics");
            DropTable("dbo.IPDFallRiskAssessmentForObstetricDatas");
        }
    }
}
