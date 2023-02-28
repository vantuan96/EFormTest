namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class observation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ObservationCharts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ObservationChartDatas",
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
                        Resp = c.String(),
                        SpO2 = c.String(),
                        RestPainScore = c.String(),
                        MovePainScore = c.String(),
                        ObservationChartId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ObservationCharts", t => t.ObservationChartId)
                .Index(t => t.ObservationChartId);
            
            CreateTable(
                "dbo.ObservationChartPulseBloodPressures",
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
                        SysBP = c.Single(nullable: false),
                        DiaBP = c.Single(nullable: false),
                        Pulse = c.Single(nullable: false),
                        ObservationChartId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ObservationCharts", t => t.ObservationChartId)
                .Index(t => t.ObservationChartId);
            
            CreateTable(
                "dbo.ObservationChartTemperatures",
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
                        Temperature = c.Single(nullable: false),
                        ObservationChartId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ObservationCharts", t => t.ObservationChartId)
                .Index(t => t.ObservationChartId);
            
            AddColumn("dbo.EDs", "ObservationChartId", c => c.Guid());
            CreateIndex("dbo.EDs", "ObservationChartId");
            AddForeignKey("dbo.EDs", "ObservationChartId", "dbo.ObservationCharts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ObservationChartTemperatures", "ObservationChartId", "dbo.ObservationCharts");
            DropForeignKey("dbo.ObservationChartPulseBloodPressures", "ObservationChartId", "dbo.ObservationCharts");
            DropForeignKey("dbo.ObservationChartDatas", "ObservationChartId", "dbo.ObservationCharts");
            DropForeignKey("dbo.EDs", "ObservationChartId", "dbo.ObservationCharts");
            DropIndex("dbo.ObservationChartTemperatures", new[] { "ObservationChartId" });
            DropIndex("dbo.ObservationChartPulseBloodPressures", new[] { "ObservationChartId" });
            DropIndex("dbo.ObservationChartDatas", new[] { "ObservationChartId" });
            DropIndex("dbo.EDs", new[] { "ObservationChartId" });
            DropColumn("dbo.EDs", "ObservationChartId");
            DropTable("dbo.ObservationChartTemperatures");
            DropTable("dbo.ObservationChartPulseBloodPressures");
            DropTable("dbo.ObservationChartDatas");
            DropTable("dbo.ObservationCharts");
        }
    }
}
