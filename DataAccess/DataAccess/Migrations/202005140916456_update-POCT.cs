namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatePOCT : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EDArterialBloodGasTestDatas", "EDPointOfCareTestingMasterDataId", c => c.Guid());
            AddColumn("dbo.EDArterialBloodGasTests", "Upload", c => c.String());
            AddColumn("dbo.EDChemicalBiologyTestDatas", "EDPointOfCareTestingMasterDataId", c => c.Guid());
            AddColumn("dbo.EDChemicalBiologyTests", "Upload", c => c.String());
            CreateIndex("dbo.EDArterialBloodGasTestDatas", "EDPointOfCareTestingMasterDataId");
            CreateIndex("dbo.EDChemicalBiologyTestDatas", "EDPointOfCareTestingMasterDataId");
            AddForeignKey("dbo.EDArterialBloodGasTestDatas", "EDPointOfCareTestingMasterDataId", "dbo.EDPointOfCareTestingMasterDatas", "Id");
            AddForeignKey("dbo.EDChemicalBiologyTestDatas", "EDPointOfCareTestingMasterDataId", "dbo.EDPointOfCareTestingMasterDatas", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EDChemicalBiologyTestDatas", "EDPointOfCareTestingMasterDataId", "dbo.EDPointOfCareTestingMasterDatas");
            DropForeignKey("dbo.EDArterialBloodGasTestDatas", "EDPointOfCareTestingMasterDataId", "dbo.EDPointOfCareTestingMasterDatas");
            DropIndex("dbo.EDChemicalBiologyTestDatas", new[] { "EDPointOfCareTestingMasterDataId" });
            DropIndex("dbo.EDArterialBloodGasTestDatas", new[] { "EDPointOfCareTestingMasterDataId" });
            DropColumn("dbo.EDChemicalBiologyTests", "Upload");
            DropColumn("dbo.EDChemicalBiologyTestDatas", "EDPointOfCareTestingMasterDataId");
            DropColumn("dbo.EDArterialBloodGasTests", "Upload");
            DropColumn("dbo.EDArterialBloodGasTestDatas", "EDPointOfCareTestingMasterDataId");
        }
    }
}
