namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upatemonitoring : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EDs", "MonitoringChartAndHandoverFormId", c => c.Guid());
            CreateIndex("dbo.EDs", "MonitoringChartAndHandoverFormId");
            AddForeignKey("dbo.EDs", "MonitoringChartAndHandoverFormId", "dbo.MonitoringChartAndHandoverForms", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EDs", "MonitoringChartAndHandoverFormId", "dbo.MonitoringChartAndHandoverForms");
            DropIndex("dbo.EDs", new[] { "MonitoringChartAndHandoverFormId" });
            DropColumn("dbo.EDs", "MonitoringChartAndHandoverFormId");
        }
    }
}
