namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changelog : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserTrackings", "Request", c => c.String());
            AddColumn("dbo.UserTrackings", "Response", c => c.String(unicode: false, storeType: "text"));
            AddColumn("dbo.UserTrackings", "Ip", c => c.String());
            DropColumn("dbo.UserTrackings", "Action");
            DropColumn("dbo.UserTrackings", "BeforeAction");
            DropColumn("dbo.UserTrackings", "AfterAction");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserTrackings", "AfterAction", c => c.String());
            AddColumn("dbo.UserTrackings", "BeforeAction", c => c.String());
            AddColumn("dbo.UserTrackings", "Action", c => c.String());
            DropColumn("dbo.UserTrackings", "Ip");
            DropColumn("dbo.UserTrackings", "Response");
            DropColumn("dbo.UserTrackings", "Request");
        }
    }
}
