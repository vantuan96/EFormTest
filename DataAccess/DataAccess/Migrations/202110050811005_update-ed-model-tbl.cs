namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateedmodeltbl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EDs", "SelfHarmRiskScreeningResults", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EDs", "SelfHarmRiskScreeningResults");
        }
    }
}
