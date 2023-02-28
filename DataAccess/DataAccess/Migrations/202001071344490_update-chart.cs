namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatechart : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ObservationChartDatas", "Other", c => c.String());
            AlterColumn("dbo.ObservationChartDatas", "SysBP", c => c.String());
            AlterColumn("dbo.ObservationChartDatas", "DiaBP", c => c.String());
            AlterColumn("dbo.ObservationChartDatas", "Pulse", c => c.String());
            AlterColumn("dbo.ObservationChartDatas", "Temperature", c => c.String());
            DropColumn("dbo.ObservationChartDatas", "Noteby");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ObservationChartDatas", "Noteby", c => c.String());
            AlterColumn("dbo.ObservationChartDatas", "Temperature", c => c.Single(nullable: false));
            AlterColumn("dbo.ObservationChartDatas", "Pulse", c => c.Single(nullable: false));
            AlterColumn("dbo.ObservationChartDatas", "DiaBP", c => c.Single(nullable: false));
            AlterColumn("dbo.ObservationChartDatas", "SysBP", c => c.Single(nullable: false));
            DropColumn("dbo.ObservationChartDatas", "Other");
        }
    }
}
