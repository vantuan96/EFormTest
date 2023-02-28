namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateversioninitassessment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OPDInitialAssessmentForShortTerms", "Version", c => c.Int(nullable: false));
            AddColumn("dbo.IPDInitialAssessmentForChemotherapies", "Version", c => c.Int(nullable: false));
            AddColumn("dbo.IPDInitialAssessmentForFrailElderlies", "Version", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.IPDInitialAssessmentForFrailElderlies", "Version");
            DropColumn("dbo.IPDInitialAssessmentForChemotherapies", "Version");
            DropColumn("dbo.OPDInitialAssessmentForShortTerms", "Version");
        }
    }
}
