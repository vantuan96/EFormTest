namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmonitoring : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MonitoringChartAndHandoverFormDatas",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTime(),
                        DeletedBy = c.String(),
                        Path = c.String(),
                        MonitoringChartAndHandoverFormId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MonitoringChartAndHandoverForms", t => t.MonitoringChartAndHandoverFormId)
                .Index(t => t.MonitoringChartAndHandoverFormId);
            
            CreateTable(
                "dbo.MonitoringChartAndHandoverForms",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTime(),
                        DeletedBy = c.String(),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MonitoringChartAndHandoverFormDatas", "MonitoringChartAndHandoverFormId", "dbo.MonitoringChartAndHandoverForms");
            DropIndex("dbo.MonitoringChartAndHandoverFormDatas", new[] { "MonitoringChartAndHandoverFormId" });
            DropTable("dbo.MonitoringChartAndHandoverForms");
            DropTable("dbo.MonitoringChartAndHandoverFormDatas");
        }
    }
}
