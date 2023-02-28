namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addsession : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Session", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Session");
        }
    }
}
