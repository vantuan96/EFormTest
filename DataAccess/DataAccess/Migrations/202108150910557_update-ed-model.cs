namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateedmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EDs", "HasFallRiskScreening", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EDs", "HasFallRiskScreening");
        }
    }
}
