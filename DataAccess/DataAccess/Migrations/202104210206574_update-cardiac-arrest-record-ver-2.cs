namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatecardiacarrestrecordver2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EIOCardiacArrestRecords", "Version", c => c.Int(nullable: false));
            AddColumn("dbo.EIOCardiacArrestRecords", "TeamLeaderTime", c => c.DateTime());
            AddColumn("dbo.EIOCardiacArrestRecords", "TeamLeaderId", c => c.Guid());
            AddColumn("dbo.EIOCardiacArrestRecords", "FormCompletedTime", c => c.DateTime());
            AddColumn("dbo.EIOCardiacArrestRecords", "FormCompletedId", c => c.Guid());
            AddColumn("dbo.EIOCardiacArrestRecordTables", "Rhythm", c => c.String());
            AddColumn("dbo.EIOCardiacArrestRecordTables", "Defib", c => c.String());
            AddColumn("dbo.EIOCardiacArrestRecordTables", "Andrenalin", c => c.String());
            AddColumn("dbo.EIOCardiacArrestRecordTables", "Amiodarone", c => c.String());
            AddColumn("dbo.EIOCardiacArrestRecordTables", "Other", c => c.String());
            CreateIndex("dbo.EIOCardiacArrestRecords", "TeamLeaderId");
            CreateIndex("dbo.EIOCardiacArrestRecords", "FormCompletedId");
            AddForeignKey("dbo.EIOCardiacArrestRecords", "FormCompletedId", "dbo.Users", "Id");
            AddForeignKey("dbo.EIOCardiacArrestRecords", "TeamLeaderId", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EIOCardiacArrestRecords", "TeamLeaderId", "dbo.Users");
            DropForeignKey("dbo.EIOCardiacArrestRecords", "FormCompletedId", "dbo.Users");
            DropIndex("dbo.EIOCardiacArrestRecords", new[] { "FormCompletedId" });
            DropIndex("dbo.EIOCardiacArrestRecords", new[] { "TeamLeaderId" });
            DropColumn("dbo.EIOCardiacArrestRecordTables", "Other");
            DropColumn("dbo.EIOCardiacArrestRecordTables", "Amiodarone");
            DropColumn("dbo.EIOCardiacArrestRecordTables", "Andrenalin");
            DropColumn("dbo.EIOCardiacArrestRecordTables", "Defib");
            DropColumn("dbo.EIOCardiacArrestRecordTables", "Rhythm");
            DropColumn("dbo.EIOCardiacArrestRecords", "FormCompletedId");
            DropColumn("dbo.EIOCardiacArrestRecords", "FormCompletedTime");
            DropColumn("dbo.EIOCardiacArrestRecords", "TeamLeaderId");
            DropColumn("dbo.EIOCardiacArrestRecords", "TeamLeaderTime");
            DropColumn("dbo.EIOCardiacArrestRecords", "Version");
        }
    }
}
