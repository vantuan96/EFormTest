namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upatelogandindex : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LogDetails", "RequestId", c => c.String(maxLength: 80, unicode: false));
            AddColumn("dbo.Logs", "RequestId", c => c.String(maxLength: 80, unicode: false));
            AlterColumn("dbo.Users", "Username", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Logs", "URI", c => c.String(maxLength: 450, unicode: false));
            CreateIndex("dbo.Users", "Username");
            CreateIndex("dbo.LogDetails", "RequestId");
            CreateIndex("dbo.Logs", "URI");
            CreateIndex("dbo.Logs", "RequestId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Logs", new[] { "RequestId" });
            DropIndex("dbo.Logs", new[] { "URI" });
            DropIndex("dbo.LogDetails", new[] { "RequestId" });
            DropIndex("dbo.Users", new[] { "Username" });
            AlterColumn("dbo.Logs", "URI", c => c.String());
            AlterColumn("dbo.Users", "Username", c => c.String());
            DropColumn("dbo.Logs", "RequestId");
            DropColumn("dbo.LogDetails", "RequestId");
        }
    }
}
