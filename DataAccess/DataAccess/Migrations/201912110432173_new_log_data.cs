namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new_log_data : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Logs", "ObjectId", c => c.Guid());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Logs", "ObjectId");
        }
    }
}
