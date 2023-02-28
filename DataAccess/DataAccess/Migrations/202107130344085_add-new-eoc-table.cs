namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addneweoctable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EOCFallRiskScreenings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Version = c.Int(nullable: false),
                        VisitId = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EOCs", t => t.VisitId)
                .Index(t => t.VisitId);
            
            CreateTable(
                "dbo.EOCInitialAssessmentForOnGoings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Version = c.Int(nullable: false),
                        VisitId = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EOCs", t => t.VisitId)
                .Index(t => t.VisitId);
            
            CreateTable(
                "dbo.EOCInitialAssessmentForShortTerms",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Version = c.Int(nullable: false),
                        VisitId = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EOCs", t => t.VisitId)
                .Index(t => t.VisitId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EOCInitialAssessmentForShortTerms", "VisitId", "dbo.EOCs");
            DropForeignKey("dbo.EOCInitialAssessmentForOnGoings", "VisitId", "dbo.EOCs");
            DropForeignKey("dbo.EOCFallRiskScreenings", "VisitId", "dbo.EOCs");
            DropIndex("dbo.EOCInitialAssessmentForShortTerms", new[] { "VisitId" });
            DropIndex("dbo.EOCInitialAssessmentForOnGoings", new[] { "VisitId" });
            DropIndex("dbo.EOCFallRiskScreenings", new[] { "VisitId" });
            DropTable("dbo.EOCInitialAssessmentForShortTerms");
            DropTable("dbo.EOCInitialAssessmentForOnGoings");
            DropTable("dbo.EOCFallRiskScreenings");
        }
    }
}
