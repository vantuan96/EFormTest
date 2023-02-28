namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changerole : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Status", c => c.String());
        }
    }
}
