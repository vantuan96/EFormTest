namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addhumanresourceassessmentshift : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HumanResourceAssessmentShifts",
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
                        ViName = c.String(),
                        EnName = c.String(),
                        StartAt = c.String(),
                        EndAt = c.String(),
                        SiteId = c.Guid(),
                        SpecialtyId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sites", t => t.SiteId)
                .ForeignKey("dbo.Specialties", t => t.SpecialtyId)
                .Index(t => t.SiteId)
                .Index(t => t.SpecialtyId);
            
            AddColumn("dbo.HumanResourceAssessmentPositions", "Order", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HumanResourceAssessmentShifts", "SpecialtyId", "dbo.Specialties");
            DropForeignKey("dbo.HumanResourceAssessmentShifts", "SiteId", "dbo.Sites");
            DropIndex("dbo.HumanResourceAssessmentShifts", new[] { "SpecialtyId" });
            DropIndex("dbo.HumanResourceAssessmentShifts", new[] { "SiteId" });
            DropColumn("dbo.HumanResourceAssessmentPositions", "Order");
            DropTable("dbo.HumanResourceAssessmentShifts");
        }
    }
}
