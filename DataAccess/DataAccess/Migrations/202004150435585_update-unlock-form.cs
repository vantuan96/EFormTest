namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateunlockform : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UnlockFormToUpdates", "VisitId", c => c.Guid());
            AddColumn("dbo.UnlockFormToUpdates", "RecordCode", c => c.String());
            DropColumn("dbo.UnlockFormToUpdates", "VisiId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UnlockFormToUpdates", "VisiId", c => c.Guid());
            DropColumn("dbo.UnlockFormToUpdates", "RecordCode");
            DropColumn("dbo.UnlockFormToUpdates", "VisitId");
        }
    }
}
