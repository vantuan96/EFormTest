namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatesitetable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sites", "ViName", c => c.String());
            AddColumn("dbo.Sites", "EnName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sites", "EnName");
            DropColumn("dbo.Sites", "ViName");
        }
    }
}
