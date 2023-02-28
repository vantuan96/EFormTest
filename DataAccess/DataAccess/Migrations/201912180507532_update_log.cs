namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_log : DbMigration
    {
        public override void Up()
        {
            //RenameColumn(table: "dbo.LogDetails", name: "LogId", newName: "Log_Id");
            //RenameIndex(table: "dbo.LogDetails", name: "IX_LogId", newName: "IX_Log_Id");
            AddColumn("dbo.LogDetails", "ModelId", c => c.Guid());
            AddColumn("dbo.LogDetails", "ModelName", c => c.String());
            AddColumn("dbo.LogDetails", "Action", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LogDetails", "Action");
            DropColumn("dbo.LogDetails", "ModelName");
            DropColumn("dbo.LogDetails", "ModelId");
            //RenameIndex(table: "dbo.LogDetails", name: "IX_Log_Id", newName: "IX_LogId");
            //RenameColumn(table: "dbo.LogDetails", name: "Log_Id", newName: "LogId");
        }
    }
}
