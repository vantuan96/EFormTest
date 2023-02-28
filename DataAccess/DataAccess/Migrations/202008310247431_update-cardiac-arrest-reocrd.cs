namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatecardiacarrestreocrd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EIOCardiacArrestRecordDatas", "EIOCardiacArrestRecordId", c => c.Guid());
            CreateIndex("dbo.EIOCardiacArrestRecordDatas", "EIOCardiacArrestRecordId");
            AddForeignKey("dbo.EIOCardiacArrestRecordDatas", "EIOCardiacArrestRecordId", "dbo.EIOCardiacArrestRecords", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EIOCardiacArrestRecordDatas", "EIOCardiacArrestRecordId", "dbo.EIOCardiacArrestRecords");
            DropIndex("dbo.EIOCardiacArrestRecordDatas", new[] { "EIOCardiacArrestRecordId" });
            DropColumn("dbo.EIOCardiacArrestRecordDatas", "EIOCardiacArrestRecordId");
        }
    }
}
