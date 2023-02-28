namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetableEDSelfHarmRiskScreeningTool : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EDSelfHarmRiskScreeningTools", "ScreeningTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EDSelfHarmRiskScreeningTools", "ScreeningTime");
        }
    }
}
