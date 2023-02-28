namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_name_initialassessment : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.InnitialAssessments", newName: "InitialAssessments");
            RenameTable(name: "dbo.InnitialAssessmentDatas", newName: "InitialAssessmentDatas");
            RenameColumn(table: "dbo.InitialAssessmentDatas", name: "InnitialAssessmentId", newName: "InitialAssessmentId");
            RenameIndex(table: "dbo.InitialAssessmentDatas", name: "IX_InnitialAssessmentId", newName: "IX_InitialAssessmentId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.InitialAssessmentDatas", name: "IX_InitialAssessmentId", newName: "IX_InnitialAssessmentId");
            RenameColumn(table: "dbo.InitialAssessmentDatas", name: "InitialAssessmentId", newName: "InnitialAssessmentId");
            RenameTable(name: "dbo.InitialAssessmentDatas", newName: "InnitialAssessmentDatas");
            RenameTable(name: "dbo.InitialAssessments", newName: "InnitialAssessments");
        }
    }
}
