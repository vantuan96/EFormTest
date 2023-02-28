namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_data : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmergencyTriageRecordDatas", "Code", c => c.String());
            AddColumn("dbo.InnitialAssessmentDatas", "Code", c => c.String());
            DropColumn("dbo.EmergencyTriageRecordDatas", "QuestionCode");
            DropColumn("dbo.EmergencyTriageRecordDatas", "AnswerCode");
            DropColumn("dbo.InnitialAssessmentDatas", "QuestionCode");
            DropColumn("dbo.InnitialAssessmentDatas", "AnswerCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.InnitialAssessmentDatas", "AnswerCode", c => c.String());
            AddColumn("dbo.InnitialAssessmentDatas", "QuestionCode", c => c.String());
            AddColumn("dbo.EmergencyTriageRecordDatas", "AnswerCode", c => c.String());
            AddColumn("dbo.EmergencyTriageRecordDatas", "QuestionCode", c => c.String());
            DropColumn("dbo.InnitialAssessmentDatas", "Code");
            DropColumn("dbo.EmergencyTriageRecordDatas", "Code");
        }
    }
}
