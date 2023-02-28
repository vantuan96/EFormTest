namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatebloodtransfusionthawedtime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EIOBloodTransfusionChecklists", "ThawedTimeAt", c => c.DateTime());
            AddColumn("dbo.EIOBloodTransfusionChecklists", "ThawedTimeTo", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EIOBloodTransfusionChecklists", "ThawedTimeTo");
            DropColumn("dbo.EIOBloodTransfusionChecklists", "ThawedTimeAt");
        }
    }
}
