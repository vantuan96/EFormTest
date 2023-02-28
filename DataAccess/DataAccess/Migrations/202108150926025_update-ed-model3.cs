namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateedmodel3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EDs", "IsHasFallRiskScreening", c => c.Boolean());
            DropColumn("dbo.EDs", "HasFallRiskScreening");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EDs", "HasFallRiskScreening", c => c.Boolean(nullable: false));
            DropColumn("dbo.EDs", "IsHasFallRiskScreening");
        }
    }
}
