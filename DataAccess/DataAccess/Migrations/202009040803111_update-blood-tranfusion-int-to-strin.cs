namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatebloodtranfusioninttostrin : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EIOBloodTransfusionChecklistDatas", "Temp", c => c.String());
            AlterColumn("dbo.EIOBloodTransfusionChecklists", "Quanlity", c => c.String());
            AlterColumn("dbo.EIOBloodTransfusionChecklists", "ActualAmountOfBloodTransmitted", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EIOBloodTransfusionChecklists", "ActualAmountOfBloodTransmitted", c => c.Int(nullable: false));
            AlterColumn("dbo.EIOBloodTransfusionChecklists", "Quanlity", c => c.Int(nullable: false));
            AlterColumn("dbo.EIOBloodTransfusionChecklistDatas", "Temp", c => c.Single(nullable: false));
        }
    }
}
