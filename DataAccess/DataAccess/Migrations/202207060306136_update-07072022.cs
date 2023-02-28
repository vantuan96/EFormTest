namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update07072022 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OPDClinicalBreastExamNotes",
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
            
            CreateTable(
                "dbo.OPDGENBRCAs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VisitId = c.Guid(),
                        UserConfirmId = c.Guid(),
                        UserConfirmAt = c.DateTime(),
                        TypeConfirm = c.String(),
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
                "dbo.ProcedureSummaryV2",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VisitId = c.Guid(),
                        VisitType = c.String(),
                        ProcedureDoctorId = c.Guid(),
                        ProcedureTime = c.DateTime(),
                        Note = c.String(),
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
                "dbo.SendMailNotifications",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FormId = c.Guid(),
                        ReceiverId = c.Guid(),
                        Subject = c.String(),
                        Body = c.String(),
                        To = c.String(),
                        Type = c.String(),
                        Status = c.String(),
                        ErrorMessenge = c.String(),
                        ErrorCount = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.IPDInitialAssessmentForNewborns", "DateOfAdmission", c => c.DateTime());
            AddColumn("dbo.IPDMedicalRecordExtenstions", "Version", c => c.Int(nullable: false));
            AddColumn("dbo.IPDMedicalRecordOfPatients", "Version", c => c.Int(nullable: false));
            AddColumn("dbo.IPDSurgeryCertificates", "FormId", c => c.Guid());
            AddColumn("dbo.OPDFallRiskScreenings", "VisitId", c => c.Guid(nullable: false));
            AddColumn("dbo.OPDFallRiskScreenings", "Version", c => c.Int(nullable: false));
            AddColumn("dbo.OPDOutpatientExaminationNotes", "AppointmentDateResult", c => c.DateTime());
            AlterColumn("dbo.VitalSignForPregnantWomen", "TransactionDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.VitalSignForPregnantWomen", "TransactionDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.OPDOutpatientExaminationNotes", "AppointmentDateResult");
            DropColumn("dbo.OPDFallRiskScreenings", "Version");
            DropColumn("dbo.OPDFallRiskScreenings", "VisitId");
            DropColumn("dbo.IPDSurgeryCertificates", "FormId");
            DropColumn("dbo.IPDMedicalRecordOfPatients", "Version");
            DropColumn("dbo.IPDMedicalRecordExtenstions", "Version");
            DropColumn("dbo.IPDInitialAssessmentForNewborns", "DateOfAdmission");
            DropTable("dbo.SendMailNotifications");
            DropTable("dbo.ProcedureSummaryV2");
            DropTable("dbo.OPDGENBRCAs");
            DropTable("dbo.OPDClinicalBreastExamNotes");
        }
    }
}
