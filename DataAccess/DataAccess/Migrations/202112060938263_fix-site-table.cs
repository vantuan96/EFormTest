namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixsitetable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Sites", "LocationCode");
            DropColumn("dbo.Sites", "LocationName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sites", "LocationName", c => c.String());
            AddColumn("dbo.Sites", "LocationCode", c => c.String());
        }
    }
}
