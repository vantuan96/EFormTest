namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update21072022 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EDFallRickScreennings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VisitId = c.Guid(nullable: false),
                        TransessionDate = c.DateTime(nullable: false),
                        ClinicName = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IPDScaleForAssessmentOfSuicideIntents",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VisitId = c.Guid(),
                        DoctorConfirmId = c.Guid(),
                        DoctorConfirmAt = c.DateTime(),
                        Order = c.Int(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.EDArterialBloodGasTests", "AllenTest", c => c.String());
            AddColumn("dbo.EDArterialBloodGasTests", "CollectionSite", c => c.String());
            AddColumn("dbo.EDChemicalBiologyTests", "Version", c => c.Int(nullable: false));
            AddColumn("dbo.EIOJointConsultationForApprovalOfSurgeries", "Version", c => c.Int(nullable: false));
            AddColumn("dbo.IPDMedicationHistories", "FormCode", c => c.String());
            AddColumn("dbo.IPDMedicationHistories", "Version", c => c.Int(nullable: false));
            AddColumn("dbo.UnlockFormToUpdates", "FormId", c => c.Guid());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UnlockFormToUpdates", "FormId");
            DropColumn("dbo.IPDMedicationHistories", "Version");
            DropColumn("dbo.IPDMedicationHistories", "FormCode");
            DropColumn("dbo.EIOJointConsultationForApprovalOfSurgeries", "Version");
            DropColumn("dbo.EDChemicalBiologyTests", "Version");
            DropColumn("dbo.EDArterialBloodGasTests", "CollectionSite");
            DropColumn("dbo.EDArterialBloodGasTests", "AllenTest");
            DropTable("dbo.IPDScaleForAssessmentOfSuicideIntents");
            DropTable("dbo.EDFallRickScreennings");
        }
    }
}
