namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateeioskintest : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.EDSkinTestResults", newName: "EIOSkinTestResults");
            RenameTable(name: "dbo.EDSkinTestResultDatas", newName: "EIOSkinTestResultDatas");
            AddColumn("dbo.EIOSkinTestResults", "VisitId", c => c.Guid());
            AddColumn("dbo.EIOSkinTestResults", "VisitTypeGroupCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EIOSkinTestResults", "VisitTypeGroupCode");
            DropColumn("dbo.EIOSkinTestResults", "VisitId");
            RenameTable(name: "dbo.EIOSkinTestResultDatas", newName: "EDSkinTestResultDatas");
            RenameTable(name: "dbo.EIOSkinTestResults", newName: "EDSkinTestResults");
        }
    }
}
