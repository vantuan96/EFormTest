namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetimebloodrequestsupplyconfirm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EIOBloodRequestSupplyAndConfirmations", "HeadOfDeptTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EIOBloodRequestSupplyAndConfirmations", "HeadOfDeptTime");
        }
    }
}
