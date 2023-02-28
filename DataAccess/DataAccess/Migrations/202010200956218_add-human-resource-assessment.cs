namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addhumanresourceassessment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HumanResourceAssessmentPositions",
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
                        EnName = c.String(),
                        ViName = c.String(),
                        SpecialtyId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Specialties", t => t.SpecialtyId)
                .Index(t => t.SpecialtyId);
            
            CreateTable(
                "dbo.HumanResourceAssessments",
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
                        StartAt = c.DateTime(),
                        EndAt = c.DateTime(),
                        Date = c.DateTime(),
                        ViName = c.String(),
                        EnName = c.String(),
                        SpecialtyId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Specialties", t => t.SpecialtyId)
                .Index(t => t.SpecialtyId);
            
            CreateTable(
                "dbo.HumanResourceAssessmentStaffs",
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
                        EnName = c.String(),
                        ViName = c.String(),
                        Type = c.String(),
                        UserId = c.Guid(),
                        HumanResourceAssessmentId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HumanResourceAssessments", t => t.HumanResourceAssessmentId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.HumanResourceAssessmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HumanResourceAssessments", "SpecialtyId", "dbo.Specialties");
            DropForeignKey("dbo.HumanResourceAssessmentStaffs", "UserId", "dbo.Users");
            DropForeignKey("dbo.HumanResourceAssessmentStaffs", "HumanResourceAssessmentId", "dbo.HumanResourceAssessments");
            DropForeignKey("dbo.HumanResourceAssessmentPositions", "SpecialtyId", "dbo.Specialties");
            DropIndex("dbo.HumanResourceAssessmentStaffs", new[] { "HumanResourceAssessmentId" });
            DropIndex("dbo.HumanResourceAssessmentStaffs", new[] { "UserId" });
            DropIndex("dbo.HumanResourceAssessments", new[] { "SpecialtyId" });
            DropIndex("dbo.HumanResourceAssessmentPositions", new[] { "SpecialtyId" });
            DropTable("dbo.HumanResourceAssessmentStaffs");
            DropTable("dbo.HumanResourceAssessments");
            DropTable("dbo.HumanResourceAssessmentPositions");
        }
    }
}
