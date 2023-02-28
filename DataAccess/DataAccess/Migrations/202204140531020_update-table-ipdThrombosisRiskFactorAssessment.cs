namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetableipdThrombosisRiskFactorAssessment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IPDThrombosisRiskFactorAssessments", "ContraindicationsForAntiCoagulant", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.IPDThrombosisRiskFactorAssessments", "ContraindicationsForAntiCoagulant");
        }
    }
}
