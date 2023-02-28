namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatelog : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Logs", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Logs", "Name");
        }
    }
}
