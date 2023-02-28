namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetblmrfix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IPDThrombosisRiskFactorAssessments", "VTERiskFactors", c => c.String());
            AddColumn("dbo.IPDThrombosisRiskFactorAssessments", "ConditionOfPatient", c => c.String());
            AddColumn("dbo.IPDThrombosisRiskFactorAssessments", "MechanicalProphylaxis", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.IPDThrombosisRiskFactorAssessments", "MechanicalProphylaxis");
            DropColumn("dbo.IPDThrombosisRiskFactorAssessments", "ConditionOfPatient");
            DropColumn("dbo.IPDThrombosisRiskFactorAssessments", "VTERiskFactors");
        }
    }
}
