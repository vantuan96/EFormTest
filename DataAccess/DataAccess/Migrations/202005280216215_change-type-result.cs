namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changetyperesult : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EDArterialBloodGasTestDatas", "Result", c => c.String());
            AlterColumn("dbo.EDChemicalBiologyTestDatas", "Result", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EDChemicalBiologyTestDatas", "Result", c => c.Single());
            AlterColumn("dbo.EDArterialBloodGasTestDatas", "Result", c => c.Single());
        }
    }
}
