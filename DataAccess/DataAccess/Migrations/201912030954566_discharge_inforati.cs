namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class discharge_inforati : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.DischargeInformationDatas", name: "DischargeInfomationId", newName: "DischargeInformationId");
            RenameIndex(table: "dbo.DischargeInformationDatas", name: "IX_DischargeInfomationId", newName: "IX_DischargeInformationId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.DischargeInformationDatas", name: "IX_DischargeInformationId", newName: "IX_DischargeInfomationId");
            RenameColumn(table: "dbo.DischargeInformationDatas", name: "DischargeInformationId", newName: "DischargeInfomationId");
        }
    }
}
