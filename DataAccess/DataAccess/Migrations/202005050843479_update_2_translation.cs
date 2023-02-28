namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_2_translation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Translations", "PID", c => c.String());
            AddColumn("dbo.Translations", "VisitCode", c => c.String());
            AddColumn("dbo.Translations", "CustomerName", c => c.String());
            AddColumn("dbo.Translations", "SiteId", c => c.Guid());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Translations", "SiteId");
            DropColumn("dbo.Translations", "CustomerName");
            DropColumn("dbo.Translations", "VisitCode");
            DropColumn("dbo.Translations", "PID");
        }
    }
}
