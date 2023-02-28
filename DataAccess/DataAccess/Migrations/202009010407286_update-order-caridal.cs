namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateordercaridal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EIOCardiacArrestRecordDatas", "Order", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EIOCardiacArrestRecordDatas", "Order");
        }
    }
}
