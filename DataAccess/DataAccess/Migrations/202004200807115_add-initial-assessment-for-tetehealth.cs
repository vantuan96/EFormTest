namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addinitialassessmentfortetehealth : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OPDInitialAssessmentForTelehealthDatas",
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
                        Value = c.String(),
                        EnValue = c.String(),
                        OPDInitialAssessmentForTelehealthId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OPDInitialAssessmentForTelehealths", t => t.OPDInitialAssessmentForTelehealthId)
                .Index(t => t.OPDInitialAssessmentForTelehealthId);
            
            CreateTable(
                "dbo.OPDInitialAssessmentForTelehealths",
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
                        AdmittedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.OPDs", "IsTelehealth", c => c.Boolean(nullable: false));
            AddColumn("dbo.OPDs", "OPDInitialAssessmentForTelehealthId", c => c.Guid());
            CreateIndex("dbo.OPDs", "OPDInitialAssessmentForTelehealthId");
            AddForeignKey("dbo.OPDs", "OPDInitialAssessmentForTelehealthId", "dbo.OPDInitialAssessmentForTelehealths", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OPDs", "OPDInitialAssessmentForTelehealthId", "dbo.OPDInitialAssessmentForTelehealths");
            DropForeignKey("dbo.OPDInitialAssessmentForTelehealthDatas", "OPDInitialAssessmentForTelehealthId", "dbo.OPDInitialAssessmentForTelehealths");
            DropIndex("dbo.OPDs", new[] { "OPDInitialAssessmentForTelehealthId" });
            DropIndex("dbo.OPDInitialAssessmentForTelehealthDatas", new[] { "OPDInitialAssessmentForTelehealthId" });
            DropColumn("dbo.OPDs", "OPDInitialAssessmentForTelehealthId");
            DropColumn("dbo.OPDs", "IsTelehealth");
            DropTable("dbo.OPDInitialAssessmentForTelehealths");
            DropTable("dbo.OPDInitialAssessmentForTelehealthDatas");
        }
    }
}
