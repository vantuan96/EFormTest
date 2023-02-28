namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removelogid : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Logs", "ObjectId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Logs", "ObjectId", c => c.Guid());
        }
    }
}
