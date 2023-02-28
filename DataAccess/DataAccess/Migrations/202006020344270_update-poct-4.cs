namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatepoct4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EDArterialBloodGasTests", "VisitId", c => c.Guid());
            AddColumn("dbo.EDArterialBloodGasTests", "VisitTypeGroupCode", c => c.String());
            AddColumn("dbo.EDChemicalBiologyTests", "VisitId", c => c.Guid());
            AddColumn("dbo.EDChemicalBiologyTests", "VisitTypeGroupCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EDChemicalBiologyTests", "VisitTypeGroupCode");
            DropColumn("dbo.EDChemicalBiologyTests", "VisitId");
            DropColumn("dbo.EDArterialBloodGasTests", "VisitTypeGroupCode");
            DropColumn("dbo.EDArterialBloodGasTests", "VisitId");
        }
    }
}
