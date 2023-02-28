namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addrecordcode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EDs", "RecordCode", c => c.String());
            AddColumn("dbo.OPDs", "RecordCode", c => c.String());
            AddColumn("dbo.UnlockFormToUpdates", "VisiId", c => c.Guid());
            AddColumn("dbo.UnlockFormToUpdates", "FormName", c => c.String());
            DropColumn("dbo.UnlockFormToUpdates", "FormId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UnlockFormToUpdates", "FormId", c => c.Guid());
            DropColumn("dbo.UnlockFormToUpdates", "FormName");
            DropColumn("dbo.UnlockFormToUpdates", "VisiId");
            DropColumn("dbo.OPDs", "RecordCode");
            DropColumn("dbo.EDs", "RecordCode");
        }
    }
}
