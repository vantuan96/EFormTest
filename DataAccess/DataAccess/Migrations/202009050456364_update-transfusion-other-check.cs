namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetransfusionothercheck : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EIOBloodTransfusionChecklists", "OtherCheckTests", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EIOBloodTransfusionChecklists", "OtherCheckTests");
        }
    }
}
