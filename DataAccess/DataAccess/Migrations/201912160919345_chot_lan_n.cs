namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chot_lan_n : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.InitialAssessments", newName: "EmergencyRecords");
            RenameTable(name: "dbo.InitialAssessmentDatas", newName: "EmergencyRecordDatas");
            RenameColumn(table: "dbo.EDs", name: "InnitialAssessmentId", newName: "EmergencyRecordId");
            RenameColumn(table: "dbo.EmergencyRecordDatas", name: "InitialAssessmentId", newName: "EmergencyRecordId");
            RenameIndex(table: "dbo.EDs", name: "IX_InnitialAssessmentId", newName: "IX_EmergencyRecordId");
            RenameIndex(table: "dbo.EmergencyRecordDatas", name: "IX_InitialAssessmentId", newName: "IX_EmergencyRecordId");
            AddColumn("dbo.EmergencyRecords", "TimeSeen", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EmergencyRecords", "TimeSeen");
            RenameIndex(table: "dbo.EmergencyRecordDatas", name: "IX_EmergencyRecordId", newName: "IX_InitialAssessmentId");
            RenameIndex(table: "dbo.EDs", name: "IX_EmergencyRecordId", newName: "IX_InnitialAssessmentId");
            RenameColumn(table: "dbo.EmergencyRecordDatas", name: "EmergencyRecordId", newName: "InitialAssessmentId");
            RenameColumn(table: "dbo.EDs", name: "EmergencyRecordId", newName: "InnitialAssessmentId");
            RenameTable(name: "dbo.EmergencyRecordDatas", newName: "InitialAssessmentDatas");
            RenameTable(name: "dbo.EmergencyRecords", newName: "InitialAssessments");
        }
    }
}
