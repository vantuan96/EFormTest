namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addversioninitassessment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EDAssessmentForRetailServicePatients", "Version", c => c.Int(nullable: false));
            AddColumn("dbo.EDEmergencyTriageRecords", "Version", c => c.Int(nullable: false));
            AddColumn("dbo.OPDInitialAssessmentForOnGoings", "Version", c => c.Int(nullable: false));
            AddColumn("dbo.IPDInitialAssessmentForAdults", "Version", c => c.Int(nullable: false));
            AddColumn("dbo.MasterDatas", "Version", c => c.String());
            AddColumn("dbo.OPDInitialAssessmentForTelehealths", "Version", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OPDInitialAssessmentForTelehealths", "Version");
            DropColumn("dbo.MasterDatas", "Version");
            DropColumn("dbo.IPDInitialAssessmentForAdults", "Version");
            DropColumn("dbo.OPDInitialAssessmentForOnGoings", "Version");
            DropColumn("dbo.EDEmergencyTriageRecords", "Version");
            DropColumn("dbo.EDAssessmentForRetailServicePatients", "Version");
        }
    }
}
