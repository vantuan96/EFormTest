namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updarwob : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IPDs", "OPDObservationChartId", c => c.Guid());
            CreateIndex("dbo.IPDs", "OPDObservationChartId");
            AddForeignKey("dbo.IPDs", "OPDObservationChartId", "dbo.OPDObservationCharts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IPDs", "OPDObservationChartId", "dbo.OPDObservationCharts");
            DropIndex("dbo.IPDs", new[] { "OPDObservationChartId" });
            DropColumn("dbo.IPDs", "OPDObservationChartId");
        }
    }
}
