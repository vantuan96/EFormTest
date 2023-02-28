namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update22082022 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EIOFormConfirms",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FormId = c.Guid(),
                        Note = c.String(),
                        ConfirmType = c.String(),
                        ConfirmBy = c.String(),
                        ConfirmAt = c.DateTime(),
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
                "dbo.PROMForCoronaryDiseases",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VisitId = c.Guid(),
                        VisitType = c.String(),
                        ProcedureConfirmId = c.Guid(),
                        ProcedureConfirmTime = c.DateTime(),
                        Note = c.String(),
                        Version = c.String(),
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
                "dbo.PROMForheartFailures",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VisitId = c.Guid(),
                        VisitType = c.String(),
                        UserConfirmId = c.Guid(),
                        UserConfirmAt = c.DateTime(),
                        Version = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Clinics", "SetUpClinicDatas", c => c.String());
            AddColumn("dbo.Charges", "Status", c => c.Int());
            AddColumn("dbo.EIOBloodProductDatas", "TransmissionTime", c => c.DateTime());
            AddColumn("dbo.EIOBloodTransfusionChecklistDatas", "Period", c => c.Int());
            AddColumn("dbo.EIOBloodTransfusionChecklists", "Version", c => c.Int(nullable: false));
            AddColumn("dbo.EIOForms", "Version", c => c.Int());
            AddColumn("dbo.IPDCoronaryInterventions", "Version", c => c.Int(nullable: false));
            AddColumn("dbo.IPDSurgeryCertificates", "Version", c => c.String());
            AddColumn("dbo.OPDOutpatientExaminationNotes", "Version", c => c.Int(nullable: false));
            AddColumn("dbo.ProcedureSummaryV2", "Version", c => c.String());
            AddColumn("dbo.SurgeryAndProcedureSummaryV3", "Version", c => c.String());
            AlterColumn("dbo.EIOBloodRequestSupplyAndConfirmations", "Number", c => c.Int());
            AlterColumn("dbo.EIOBloodRequestSupplyAndConfirmations", "TransfusionTime", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EIOBloodRequestSupplyAndConfirmations", "TransfusionTime", c => c.Int(nullable: false));
            AlterColumn("dbo.EIOBloodRequestSupplyAndConfirmations", "Number", c => c.Int(nullable: false));
            DropColumn("dbo.SurgeryAndProcedureSummaryV3", "Version");
            DropColumn("dbo.ProcedureSummaryV2", "Version");
            DropColumn("dbo.OPDOutpatientExaminationNotes", "Version");
            DropColumn("dbo.IPDSurgeryCertificates", "Version");
            DropColumn("dbo.IPDCoronaryInterventions", "Version");
            DropColumn("dbo.EIOForms", "Version");
            DropColumn("dbo.EIOBloodTransfusionChecklists", "Version");
            DropColumn("dbo.EIOBloodTransfusionChecklistDatas", "Period");
            DropColumn("dbo.EIOBloodProductDatas", "TransmissionTime");
            DropColumn("dbo.Charges", "Status");
            DropColumn("dbo.Clinics", "SetUpClinicDatas");
            DropTable("dbo.PROMForheartFailures");
            DropTable("dbo.PROMForCoronaryDiseases");
            DropTable("dbo.EIOFormConfirms");
        }
    }
}
