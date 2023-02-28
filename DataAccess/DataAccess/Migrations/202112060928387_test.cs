namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sites", "LocationCode", c => c.String());
            AddColumn("dbo.Sites", "LocationName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sites", "LocationName");
            DropColumn("dbo.Sites", "LocationCode");
        }
    }
}
