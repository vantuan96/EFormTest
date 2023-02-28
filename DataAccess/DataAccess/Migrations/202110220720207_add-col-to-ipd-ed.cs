namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcoltoipded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EDs", "Room", c => c.String());
            AddColumn("dbo.IPDs", "Room", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.IPDs", "Room");
            DropColumn("dbo.EDs", "Room");
        }
    }
}
