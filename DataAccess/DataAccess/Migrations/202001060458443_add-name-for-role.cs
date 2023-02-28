namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnameforrole : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Roles", "ViName", c => c.String());
            AddColumn("dbo.Roles", "EnName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Roles", "EnName");
            DropColumn("dbo.Roles", "ViName");
        }
    }
}
