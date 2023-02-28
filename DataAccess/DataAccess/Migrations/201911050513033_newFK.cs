namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newFK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.InnitialAssessmentDatas", "InnitialAssessment_Id1", "dbo.InnitialAssessments");
            DropForeignKey("dbo.InnitialAssessmentDatas", "InnitialAssessment_Id2", "dbo.InnitialAssessments");
            DropIndex("dbo.EDs", new[] { "Customer_Id" });
            DropIndex("dbo.EDs", new[] { "EmergencyTriageRecord_Id" });
            DropIndex("dbo.EDs", new[] { "InnitialAssessment_Id" });
            DropIndex("dbo.EDs", new[] { "PatientProgressNote_Id" });
            DropIndex("dbo.EDs", new[] { "PreOperativeProcedureHandoverChecklist_Id" });
            DropIndex("dbo.EDs", new[] { "Site_Id" });
            DropIndex("dbo.EmergencyTriageRecordDatas", new[] { "EmergencyTriageRecord_Id" });
            DropIndex("dbo.Orders", new[] { "EmergencyTriageRecord_Id" });
            DropIndex("dbo.VitalSigns", new[] { "EmergencyTriageRecord_Id" });
            DropIndex("dbo.InnitialAssessmentDatas", new[] { "InnitialAssessment_Id" });
            DropIndex("dbo.InnitialAssessmentDatas", new[] { "InnitialAssessment_Id1" });
            DropIndex("dbo.InnitialAssessmentDatas", new[] { "InnitialAssessment_Id2" });
            DropIndex("dbo.PatientProgressNoteDatas", new[] { "PatientProgressNote_Id" });
            DropIndex("dbo.PreOperativeProcedureHandoverChecklistDatas", new[] { "PreOperativeProcedureHandoverChecklist_Id" });
            DropColumn("dbo.EDs", "CustomerId");
            DropColumn("dbo.EDs", "EmergencyTriageRecordId");
            DropColumn("dbo.EDs", "InnitialAssessmentId");
            DropColumn("dbo.EDs", "PatientProgressNoteId");
            DropColumn("dbo.EDs", "PreOperativeProcedureHandoverChecklistId");
            DropColumn("dbo.EDs", "SiteId");
            DropColumn("dbo.EmergencyTriageRecordDatas", "EmergencyTriageRecordId");
            DropColumn("dbo.Orders", "EmergencyTriageRecordId");
            DropColumn("dbo.VitalSigns", "EmergencyTriageRecordId");
            DropColumn("dbo.InnitialAssessmentDatas", "InnitialAssessmentId");
            DropColumn("dbo.PatientProgressNoteDatas", "PatientProgressNoteId");
            DropColumn("dbo.PreOperativeProcedureHandoverChecklistDatas", "PreOperativeProcedureHandoverChecklistId");
            RenameColumn(table: "dbo.EDs", name: "Customer_Id", newName: "CustomerId");
            RenameColumn(table: "dbo.EDs", name: "EmergencyTriageRecord_Id", newName: "EmergencyTriageRecordId");
            RenameColumn(table: "dbo.EDs", name: "InnitialAssessment_Id", newName: "InnitialAssessmentId");
            RenameColumn(table: "dbo.EDs", name: "PatientProgressNote_Id", newName: "PatientProgressNoteId");
            RenameColumn(table: "dbo.EDs", name: "PreOperativeProcedureHandoverChecklist_Id", newName: "PreOperativeProcedureHandoverChecklistId");
            RenameColumn(table: "dbo.EDs", name: "Site_Id", newName: "SiteId");
            RenameColumn(table: "dbo.EmergencyTriageRecordDatas", name: "EmergencyTriageRecord_Id", newName: "EmergencyTriageRecordId");
            RenameColumn(table: "dbo.Orders", name: "EmergencyTriageRecord_Id", newName: "EmergencyTriageRecordId");
            RenameColumn(table: "dbo.VitalSigns", name: "EmergencyTriageRecord_Id", newName: "EmergencyTriageRecordId");
            RenameColumn(table: "dbo.InnitialAssessmentDatas", name: "InnitialAssessment_Id", newName: "InnitialAssessmentId");
            RenameColumn(table: "dbo.PatientProgressNoteDatas", name: "PatientProgressNote_Id", newName: "PatientProgressNoteId");
            RenameColumn(table: "dbo.PreOperativeProcedureHandoverChecklistDatas", name: "PreOperativeProcedureHandoverChecklist_Id", newName: "PreOperativeProcedureHandoverChecklistId");
            AlterColumn("dbo.EDs", "SiteId", c => c.Guid());
            AlterColumn("dbo.EDs", "CustomerId", c => c.Guid());
            AlterColumn("dbo.EDs", "EmergencyTriageRecordId", c => c.Guid());
            AlterColumn("dbo.EDs", "InnitialAssessmentId", c => c.Guid());
            AlterColumn("dbo.EDs", "PatientProgressNoteId", c => c.Guid());
            AlterColumn("dbo.EDs", "PreOperativeProcedureHandoverChecklistId", c => c.Guid());
            AlterColumn("dbo.EmergencyTriageRecordDatas", "EmergencyTriageRecordId", c => c.Guid());
            AlterColumn("dbo.Orders", "EmergencyTriageRecordId", c => c.Guid());
            AlterColumn("dbo.VitalSigns", "EmergencyTriageRecordId", c => c.Guid());
            AlterColumn("dbo.InnitialAssessmentDatas", "InnitialAssessmentId", c => c.Guid());
            AlterColumn("dbo.PatientProgressNoteDatas", "PatientProgressNoteId", c => c.Guid());
            AlterColumn("dbo.PreOperativeProcedureHandoverChecklistDatas", "PreOperativeProcedureHandoverChecklistId", c => c.Guid());
            CreateIndex("dbo.EDs", "SiteId");
            CreateIndex("dbo.EDs", "CustomerId");
            CreateIndex("dbo.EDs", "EmergencyTriageRecordId");
            CreateIndex("dbo.EDs", "InnitialAssessmentId");
            CreateIndex("dbo.EDs", "PatientProgressNoteId");
            CreateIndex("dbo.EDs", "PreOperativeProcedureHandoverChecklistId");
            CreateIndex("dbo.EmergencyTriageRecordDatas", "EmergencyTriageRecordId");
            CreateIndex("dbo.Orders", "EmergencyTriageRecordId");
            CreateIndex("dbo.VitalSigns", "EmergencyTriageRecordId");
            CreateIndex("dbo.InnitialAssessmentDatas", "InnitialAssessmentId");
            CreateIndex("dbo.PatientProgressNoteDatas", "PatientProgressNoteId");
            CreateIndex("dbo.PreOperativeProcedureHandoverChecklistDatas", "PreOperativeProcedureHandoverChecklistId");
            DropColumn("dbo.InnitialAssessmentDatas", "InnitialAssessment_Id1");
            DropColumn("dbo.InnitialAssessmentDatas", "InnitialAssessment_Id2");
        }
        
        public override void Down()
        {
            AddColumn("dbo.InnitialAssessmentDatas", "InnitialAssessment_Id2", c => c.Guid());
            AddColumn("dbo.InnitialAssessmentDatas", "InnitialAssessment_Id1", c => c.Guid());
            DropIndex("dbo.PreOperativeProcedureHandoverChecklistDatas", new[] { "PreOperativeProcedureHandoverChecklistId" });
            DropIndex("dbo.PatientProgressNoteDatas", new[] { "PatientProgressNoteId" });
            DropIndex("dbo.InnitialAssessmentDatas", new[] { "InnitialAssessmentId" });
            DropIndex("dbo.VitalSigns", new[] { "EmergencyTriageRecordId" });
            DropIndex("dbo.Orders", new[] { "EmergencyTriageRecordId" });
            DropIndex("dbo.EmergencyTriageRecordDatas", new[] { "EmergencyTriageRecordId" });
            DropIndex("dbo.EDs", new[] { "PreOperativeProcedureHandoverChecklistId" });
            DropIndex("dbo.EDs", new[] { "PatientProgressNoteId" });
            DropIndex("dbo.EDs", new[] { "InnitialAssessmentId" });
            DropIndex("dbo.EDs", new[] { "EmergencyTriageRecordId" });
            DropIndex("dbo.EDs", new[] { "CustomerId" });
            DropIndex("dbo.EDs", new[] { "SiteId" });
            AlterColumn("dbo.PreOperativeProcedureHandoverChecklistDatas", "PreOperativeProcedureHandoverChecklistId", c => c.Int());
            AlterColumn("dbo.PatientProgressNoteDatas", "PatientProgressNoteId", c => c.Int());
            AlterColumn("dbo.InnitialAssessmentDatas", "InnitialAssessmentId", c => c.Int());
            AlterColumn("dbo.VitalSigns", "EmergencyTriageRecordId", c => c.Int());
            AlterColumn("dbo.Orders", "EmergencyTriageRecordId", c => c.Int());
            AlterColumn("dbo.EmergencyTriageRecordDatas", "EmergencyTriageRecordId", c => c.Int());
            AlterColumn("dbo.EDs", "PreOperativeProcedureHandoverChecklistId", c => c.Int());
            AlterColumn("dbo.EDs", "PatientProgressNoteId", c => c.Int());
            AlterColumn("dbo.EDs", "InnitialAssessmentId", c => c.Int());
            AlterColumn("dbo.EDs", "EmergencyTriageRecordId", c => c.Int());
            AlterColumn("dbo.EDs", "CustomerId", c => c.Int());
            AlterColumn("dbo.EDs", "SiteId", c => c.Int());
            RenameColumn(table: "dbo.PreOperativeProcedureHandoverChecklistDatas", name: "PreOperativeProcedureHandoverChecklistId", newName: "PreOperativeProcedureHandoverChecklist_Id");
            RenameColumn(table: "dbo.PatientProgressNoteDatas", name: "PatientProgressNoteId", newName: "PatientProgressNote_Id");
            RenameColumn(table: "dbo.InnitialAssessmentDatas", name: "InnitialAssessmentId", newName: "InnitialAssessment_Id");
            RenameColumn(table: "dbo.VitalSigns", name: "EmergencyTriageRecordId", newName: "EmergencyTriageRecord_Id");
            RenameColumn(table: "dbo.Orders", name: "EmergencyTriageRecordId", newName: "EmergencyTriageRecord_Id");
            RenameColumn(table: "dbo.EmergencyTriageRecordDatas", name: "EmergencyTriageRecordId", newName: "EmergencyTriageRecord_Id");
            RenameColumn(table: "dbo.EDs", name: "SiteId", newName: "Site_Id");
            RenameColumn(table: "dbo.EDs", name: "PreOperativeProcedureHandoverChecklistId", newName: "PreOperativeProcedureHandoverChecklist_Id");
            RenameColumn(table: "dbo.EDs", name: "PatientProgressNoteId", newName: "PatientProgressNote_Id");
            RenameColumn(table: "dbo.EDs", name: "InnitialAssessmentId", newName: "InnitialAssessment_Id");
            RenameColumn(table: "dbo.EDs", name: "EmergencyTriageRecordId", newName: "EmergencyTriageRecord_Id");
            RenameColumn(table: "dbo.EDs", name: "CustomerId", newName: "Customer_Id");
            AddColumn("dbo.PreOperativeProcedureHandoverChecklistDatas", "PreOperativeProcedureHandoverChecklistId", c => c.Int());
            AddColumn("dbo.PatientProgressNoteDatas", "PatientProgressNoteId", c => c.Int());
            AddColumn("dbo.InnitialAssessmentDatas", "InnitialAssessmentId", c => c.Int());
            AddColumn("dbo.VitalSigns", "EmergencyTriageRecordId", c => c.Int());
            AddColumn("dbo.Orders", "EmergencyTriageRecordId", c => c.Int());
            AddColumn("dbo.EmergencyTriageRecordDatas", "EmergencyTriageRecordId", c => c.Int());
            AddColumn("dbo.EDs", "SiteId", c => c.Int());
            AddColumn("dbo.EDs", "PreOperativeProcedureHandoverChecklistId", c => c.Int());
            AddColumn("dbo.EDs", "PatientProgressNoteId", c => c.Int());
            AddColumn("dbo.EDs", "InnitialAssessmentId", c => c.Int());
            AddColumn("dbo.EDs", "EmergencyTriageRecordId", c => c.Int());
            AddColumn("dbo.EDs", "CustomerId", c => c.Int());
            CreateIndex("dbo.PreOperativeProcedureHandoverChecklistDatas", "PreOperativeProcedureHandoverChecklist_Id");
            CreateIndex("dbo.PatientProgressNoteDatas", "PatientProgressNote_Id");
            CreateIndex("dbo.InnitialAssessmentDatas", "InnitialAssessment_Id2");
            CreateIndex("dbo.InnitialAssessmentDatas", "InnitialAssessment_Id1");
            CreateIndex("dbo.InnitialAssessmentDatas", "InnitialAssessment_Id");
            CreateIndex("dbo.VitalSigns", "EmergencyTriageRecord_Id");
            CreateIndex("dbo.Orders", "EmergencyTriageRecord_Id");
            CreateIndex("dbo.EmergencyTriageRecordDatas", "EmergencyTriageRecord_Id");
            CreateIndex("dbo.EDs", "Site_Id");
            CreateIndex("dbo.EDs", "PreOperativeProcedureHandoverChecklist_Id");
            CreateIndex("dbo.EDs", "PatientProgressNote_Id");
            CreateIndex("dbo.EDs", "InnitialAssessment_Id");
            CreateIndex("dbo.EDs", "EmergencyTriageRecord_Id");
            CreateIndex("dbo.EDs", "Customer_Id");
            AddForeignKey("dbo.InnitialAssessmentDatas", "InnitialAssessment_Id2", "dbo.InnitialAssessments", "Id");
            AddForeignKey("dbo.InnitialAssessmentDatas", "InnitialAssessment_Id1", "dbo.InnitialAssessments", "Id");
        }
    }
}
