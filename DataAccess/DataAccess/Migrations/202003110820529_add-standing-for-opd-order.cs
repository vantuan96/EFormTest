namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addstandingforopdorder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "StandingOrderMasterDataId", c => c.Guid());
            CreateIndex("dbo.Orders", "StandingOrderMasterDataId");
            AddForeignKey("dbo.Orders", "StandingOrderMasterDataId", "dbo.StandingOrderMasterDatas", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "StandingOrderMasterDataId", "dbo.StandingOrderMasterDatas");
            DropIndex("dbo.Orders", new[] { "StandingOrderMasterDataId" });
            DropColumn("dbo.Orders", "StandingOrderMasterDataId");
        }
    }
}
