namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatelocationunit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sites", "LocationUnit", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sites", "LocationUnit");
        }
    }
}
