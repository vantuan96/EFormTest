namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatesprint18 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IPDCoronaryInterventions",
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
                "dbo.IPDMonitoringSheetForPatientsWithExtravasationOfCancerDrugs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VisitId = c.Guid(),
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
                "dbo.IPDThrombosisRiskFactorAssessmentForGeneralSurgeryPatients",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StartDate = c.DateTime(),
                        FinishDate = c.DateTime(),
                        CapriniScoreCalculator = c.String(),
                        IndividualBleedingRiskScore = c.String(),
                        BaselineSurgicalBleedingRisk = c.String(),
                        AntiFreeze = c.String(),
                        MechanicalProphylaxis = c.String(),
                        ThromboembolismProphylaxis = c.String(),
                        VisitId = c.Guid(nullable: false),
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
                "dbo.VitalSignForPregnantWomen",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VisitId = c.Guid(),
                        Note = c.String(),
                        TransactionDate = c.DateTime(nullable: false),
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
                "dbo.TableDatas",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Code = c.String(),
                        Value = c.String(),
                        Id2 = c.Guid(nullable: false),
                        FormCode = c.String(),
                        IdRow = c.Guid(nullable: false),
                        CreatedRowAt = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Services", "IsDiagnosticReporting", c => c.Boolean(nullable: false));
            AddColumn("dbo.EIOForms", "FormId", c => c.Guid());
            AddColumn("dbo.EIOForms", "FormCode", c => c.String());
            AddColumn("dbo.IPDs", "IsDraft", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.IPDs", "IsDraft");
            DropColumn("dbo.EIOForms", "FormCode");
            DropColumn("dbo.EIOForms", "FormId");
            DropColumn("dbo.Services", "IsDiagnosticReporting");
            DropTable("dbo.TableDatas");
            DropTable("dbo.VitalSignForPregnantWomen");
            DropTable("dbo.IPDThrombosisRiskFactorAssessmentForGeneralSurgeryPatients");
            DropTable("dbo.IPDMonitoringSheetForPatientsWithExtravasationOfCancerDrugs");
            DropTable("dbo.IPDCoronaryInterventions");
        }
    }
}
