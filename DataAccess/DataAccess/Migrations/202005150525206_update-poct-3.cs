namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatepoct3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EDArterialBloodGasTestDatas", "Result", c => c.Single());
            AddColumn("dbo.EDChemicalBiologyTestDatas", "Result", c => c.Single());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EDChemicalBiologyTestDatas", "Result");
            DropColumn("dbo.EDArterialBloodGasTestDatas", "Result");
        }
    }
}
