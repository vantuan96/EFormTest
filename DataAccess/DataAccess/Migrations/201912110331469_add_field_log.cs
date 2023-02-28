namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_field_log : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Logs", "Action", c => c.String());
            AddColumn("dbo.Logs", "URI", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Logs", "URI");
            DropColumn("dbo.Logs", "Action");
        }
    }
}
