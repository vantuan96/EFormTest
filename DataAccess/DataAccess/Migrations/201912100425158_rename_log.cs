namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rename_log : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UserTrackings", newName: "Logs");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Logs", newName: "UserTrackings");
        }
    }
}
