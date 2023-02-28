namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addreasonandprimarynurse : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Logs", "Reason", c => c.String());
            AddColumn("dbo.OPDs", "PrimaryNurseId", c => c.Guid());
            CreateIndex("dbo.OPDs", "PrimaryNurseId");
            AddForeignKey("dbo.OPDs", "PrimaryNurseId", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OPDs", "PrimaryNurseId", "dbo.Users");
            DropIndex("dbo.OPDs", new[] { "PrimaryNurseId" });
            DropColumn("dbo.OPDs", "PrimaryNurseId");
            DropColumn("dbo.Logs", "Reason");
        }
    }
}
