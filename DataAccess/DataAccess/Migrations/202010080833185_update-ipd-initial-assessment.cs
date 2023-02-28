namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateipdinitialassessment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IPDInitialAssessmentForChemotherapyDatas", "Code", c => c.String());
            AddColumn("dbo.IPDInitialAssessmentForChemotherapyDatas", "Value", c => c.String());
            AddColumn("dbo.IPDInitialAssessmentForFrailElderlyDatas", "Code", c => c.String());
            AddColumn("dbo.IPDInitialAssessmentForFrailElderlyDatas", "Value", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.IPDInitialAssessmentForFrailElderlyDatas", "Value");
            DropColumn("dbo.IPDInitialAssessmentForFrailElderlyDatas", "Code");
            DropColumn("dbo.IPDInitialAssessmentForChemotherapyDatas", "Value");
            DropColumn("dbo.IPDInitialAssessmentForChemotherapyDatas", "Code");
        }
    }
}
