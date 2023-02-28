namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addambulancerunreport : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EDAmbulanceRunReportDatas",
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
                        Form = c.String(),
                        EDAmbulanceRunReportId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EDAmbulanceRunReports", t => t.EDAmbulanceRunReportId)
                .Index(t => t.EDAmbulanceRunReportId);
            
            CreateTable(
                "dbo.EDAmbulanceRunReports",
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
                        AssessmentNurseTime = c.DateTime(),
                        AssessmentNurseId = c.Guid(),
                        AssessmentPhysicianTime = c.DateTime(),
                        AssessmentPhysicianId = c.Guid(),
                        TransferTime = c.DateTime(),
                        TransferId = c.Guid(),
                        AdmitTime = c.DateTime(),
                        AdmitId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AdmitId)
                .ForeignKey("dbo.Users", t => t.AssessmentNurseId)
                .ForeignKey("dbo.Users", t => t.AssessmentPhysicianId)
                .ForeignKey("dbo.Users", t => t.TransferId)
                .Index(t => t.AssessmentNurseId)
                .Index(t => t.AssessmentPhysicianId)
                .Index(t => t.TransferId)
                .Index(t => t.AdmitId);
            
            CreateTable(
                "dbo.EDAmbulanceTransferPatientsRecords",
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
                        Time = c.DateTime(),
                        BP = c.String(),
                        Pulse = c.String(),
                        RespRate = c.String(),
                        PatternOfRespiration = c.String(),
                        SpO2 = c.String(),
                        HR = c.String(),
                        Procedure = c.String(),
                        Drug = c.String(),
                        Dose = c.String(),
                        Route = c.String(),
                        MedicationMasterdataId = c.Guid(),
                        EDAmbulanceRunReportId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EDAmbulanceRunReports", t => t.EDAmbulanceRunReportId)
                .ForeignKey("dbo.MedicationMasterdatas", t => t.MedicationMasterdataId)
                .Index(t => t.MedicationMasterdataId)
                .Index(t => t.EDAmbulanceRunReportId);
            
            AddColumn("dbo.EDs", "EDAmbulanceRunReportId", c => c.Guid());
            CreateIndex("dbo.EDs", "EDAmbulanceRunReportId");
            AddForeignKey("dbo.EDs", "EDAmbulanceRunReportId", "dbo.EDAmbulanceRunReports", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EDs", "EDAmbulanceRunReportId", "dbo.EDAmbulanceRunReports");
            DropForeignKey("dbo.EDAmbulanceRunReports", "TransferId", "dbo.Users");
            DropForeignKey("dbo.EDAmbulanceTransferPatientsRecords", "MedicationMasterdataId", "dbo.MedicationMasterdatas");
            DropForeignKey("dbo.EDAmbulanceTransferPatientsRecords", "EDAmbulanceRunReportId", "dbo.EDAmbulanceRunReports");
            DropForeignKey("dbo.EDAmbulanceRunReportDatas", "EDAmbulanceRunReportId", "dbo.EDAmbulanceRunReports");
            DropForeignKey("dbo.EDAmbulanceRunReports", "AssessmentPhysicianId", "dbo.Users");
            DropForeignKey("dbo.EDAmbulanceRunReports", "AssessmentNurseId", "dbo.Users");
            DropForeignKey("dbo.EDAmbulanceRunReports", "AdmitId", "dbo.Users");
            DropIndex("dbo.EDs", new[] { "EDAmbulanceRunReportId" });
            DropIndex("dbo.EDAmbulanceTransferPatientsRecords", new[] { "EDAmbulanceRunReportId" });
            DropIndex("dbo.EDAmbulanceTransferPatientsRecords", new[] { "MedicationMasterdataId" });
            DropIndex("dbo.EDAmbulanceRunReports", new[] { "AdmitId" });
            DropIndex("dbo.EDAmbulanceRunReports", new[] { "TransferId" });
            DropIndex("dbo.EDAmbulanceRunReports", new[] { "AssessmentPhysicianId" });
            DropIndex("dbo.EDAmbulanceRunReports", new[] { "AssessmentNurseId" });
            DropIndex("dbo.EDAmbulanceRunReportDatas", new[] { "EDAmbulanceRunReportId" });
            DropColumn("dbo.EDs", "EDAmbulanceRunReportId");
            DropTable("dbo.EDAmbulanceTransferPatientsRecords");
            DropTable("dbo.EDAmbulanceRunReports");
            DropTable("dbo.EDAmbulanceRunReportDatas");
        }
    }
}
