namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmortalityreport : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.EmergencyTriageRecords", newName: "EDEmergencyTriageRecords");
            RenameTable(name: "dbo.EmergencyTriageRecordDatas", newName: "EDEmergencyTriageRecordDatas");
            RenameTable(name: "dbo.DischargeInformations", newName: "EDDischargeInformations");
            RenameTable(name: "dbo.DischargeInformationDatas", newName: "EDDischargeInformationDatas");
            RenameTable(name: "dbo.EmergencyRecords", newName: "EDEmergencyRecords");
            RenameTable(name: "dbo.EmergencyRecordDatas", newName: "EDEmergencyRecordDatas");
            RenameTable(name: "dbo.HandOverCheckLists", newName: "EDHandOverCheckLists");
            RenameTable(name: "dbo.HandOverCheckListDatas", newName: "EDHandOverCheckListDatas");
            RenameTable(name: "dbo.MonitoringChartAndHandoverForms", newName: "EDMonitoringChartAndHandoverForms");
            RenameTable(name: "dbo.MonitoringChartAndHandoverFormDatas", newName: "EDMonitoringChartAndHandoverFormDatas");
            RenameTable(name: "dbo.ObservationCharts", newName: "EDObservationCharts");
            RenameTable(name: "dbo.ObservationChartDatas", newName: "EDObservationChartDatas");
            RenameTable(name: "dbo.PatientProgressNotes", newName: "EDPatientProgressNotes");
            RenameTable(name: "dbo.PatientProgressNoteDatas", newName: "EDPatientProgressNoteDatas");
            RenameTable(name: "dbo.PreOperativeProcedureHandoverChecklists", newName: "EDPreOperativeProcedureHandoverChecklists");
            RenameTable(name: "dbo.PreOperativeProcedureHandoverChecklistDatas", newName: "EDPreOperativeProcedureHandoverChecklistDatas");
            RenameTable(name: "dbo.SpongeSharpsAndInstrumentsCountsSheets", newName: "EDSpongeSharpsAndInstrumentsCountsSheets");
            RenameTable(name: "dbo.SpongeSharpsAndInstrumentsCountsSheetDatas", newName: "EDSpongeSharpsAndInstrumentsCountsSheetDatas");
            CreateTable(
                "dbo.EDMortalityReports",
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
                        DeathAt = c.DateTime(),
                        Reason = c.String(),
                        PastMedicalHistory = c.String(),
                        Status = c.String(),
                        Diagnosis = c.String(),
                        Progress = c.String(),
                        Welcome = c.String(),
                        Assessment = c.String(),
                        TreatmentAndProcedures = c.String(),
                        Care = c.String(),
                        RelationShip = c.String(),
                        Extra = c.String(),
                        Conclusion = c.String(),
                        ChairmanTime = c.DateTime(),
                        ChairmanId = c.Guid(),
                        SecretaryTime = c.DateTime(),
                        SecretaryId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.ChairmanId)
                .ForeignKey("dbo.Users", t => t.SecretaryId)
                .Index(t => t.ChairmanId)
                .Index(t => t.SecretaryId);
            
            CreateTable(
                "dbo.EDMortalityReportMembers",
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
                        MemberId = c.Guid(),
                        EDMortalityReportId = c.Guid(),
                        ConfirmTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EDMortalityReports", t => t.EDMortalityReportId)
                .ForeignKey("dbo.Users", t => t.MemberId)
                .Index(t => t.MemberId)
                .Index(t => t.EDMortalityReportId);
            
            AddColumn("dbo.EDs", "EDMortalityReportId", c => c.Guid());
            CreateIndex("dbo.EDs", "EDMortalityReportId");
            AddForeignKey("dbo.EDs", "EDMortalityReportId", "dbo.EDMortalityReports", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EDs", "EDMortalityReportId", "dbo.EDMortalityReports");
            DropForeignKey("dbo.EDMortalityReports", "SecretaryId", "dbo.Users");
            DropForeignKey("dbo.EDMortalityReportMembers", "MemberId", "dbo.Users");
            DropForeignKey("dbo.EDMortalityReportMembers", "EDMortalityReportId", "dbo.EDMortalityReports");
            DropForeignKey("dbo.EDMortalityReports", "ChairmanId", "dbo.Users");
            DropIndex("dbo.EDMortalityReportMembers", new[] { "EDMortalityReportId" });
            DropIndex("dbo.EDMortalityReportMembers", new[] { "MemberId" });
            DropIndex("dbo.EDMortalityReports", new[] { "SecretaryId" });
            DropIndex("dbo.EDMortalityReports", new[] { "ChairmanId" });
            DropIndex("dbo.EDs", new[] { "EDMortalityReportId" });
            DropColumn("dbo.EDs", "EDMortalityReportId");
            DropTable("dbo.EDMortalityReportMembers");
            DropTable("dbo.EDMortalityReports");
            RenameTable(name: "dbo.EDSpongeSharpsAndInstrumentsCountsSheetDatas", newName: "SpongeSharpsAndInstrumentsCountsSheetDatas");
            RenameTable(name: "dbo.EDSpongeSharpsAndInstrumentsCountsSheets", newName: "SpongeSharpsAndInstrumentsCountsSheets");
            RenameTable(name: "dbo.EDPreOperativeProcedureHandoverChecklistDatas", newName: "PreOperativeProcedureHandoverChecklistDatas");
            RenameTable(name: "dbo.EDPreOperativeProcedureHandoverChecklists", newName: "PreOperativeProcedureHandoverChecklists");
            RenameTable(name: "dbo.EDPatientProgressNoteDatas", newName: "PatientProgressNoteDatas");
            RenameTable(name: "dbo.EDPatientProgressNotes", newName: "PatientProgressNotes");
            RenameTable(name: "dbo.EDObservationChartDatas", newName: "ObservationChartDatas");
            RenameTable(name: "dbo.EDObservationCharts", newName: "ObservationCharts");
            RenameTable(name: "dbo.EDMonitoringChartAndHandoverFormDatas", newName: "MonitoringChartAndHandoverFormDatas");
            RenameTable(name: "dbo.EDMonitoringChartAndHandoverForms", newName: "MonitoringChartAndHandoverForms");
            RenameTable(name: "dbo.EDHandOverCheckListDatas", newName: "HandOverCheckListDatas");
            RenameTable(name: "dbo.EDHandOverCheckLists", newName: "HandOverCheckLists");
            RenameTable(name: "dbo.EDEmergencyRecordDatas", newName: "EmergencyRecordDatas");
            RenameTable(name: "dbo.EDEmergencyRecords", newName: "EmergencyRecords");
            RenameTable(name: "dbo.EDDischargeInformationDatas", newName: "DischargeInformationDatas");
            RenameTable(name: "dbo.EDDischargeInformations", newName: "DischargeInformations");
            RenameTable(name: "dbo.EDEmergencyTriageRecordDatas", newName: "EmergencyTriageRecordDatas");
            RenameTable(name: "dbo.EDEmergencyTriageRecords", newName: "EmergencyTriageRecords");
        }
    }
}
