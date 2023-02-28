namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ObservationChartPulseBloodPressures", "ObservationChartId", "dbo.ObservationCharts");
            DropForeignKey("dbo.ObservationChartTemperatures", "ObservationChartId", "dbo.ObservationCharts");
            DropIndex("dbo.ObservationChartPulseBloodPressures", new[] { "ObservationChartId" });
            DropIndex("dbo.ObservationChartTemperatures", new[] { "ObservationChartId" });
            AddColumn("dbo.ObservationChartDatas", "SysBP", c => c.Single(nullable: false));
            AddColumn("dbo.ObservationChartDatas", "DiaBP", c => c.Single(nullable: false));
            AddColumn("dbo.ObservationChartDatas", "Pulse", c => c.Single(nullable: false));
            AddColumn("dbo.ObservationChartDatas", "Temperature", c => c.Single(nullable: false));
            AddColumn("dbo.ObservationChartDatas", "Noteby", c => c.String());
            DropTable("dbo.ObservationChartPulseBloodPressures");
            DropTable("dbo.ObservationChartTemperatures");
        }
        
        public override void Down()
        {
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
                        NoteAt = c.DateTime(),
                        ObservationChartId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        NoteAt = c.DateTime(),
                        ObservationChartId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.ObservationChartDatas", "Noteby");
            DropColumn("dbo.ObservationChartDatas", "Temperature");
            DropColumn("dbo.ObservationChartDatas", "Pulse");
            DropColumn("dbo.ObservationChartDatas", "DiaBP");
            DropColumn("dbo.ObservationChartDatas", "SysBP");
            CreateIndex("dbo.ObservationChartTemperatures", "ObservationChartId");
            CreateIndex("dbo.ObservationChartPulseBloodPressures", "ObservationChartId");
            AddForeignKey("dbo.ObservationChartTemperatures", "ObservationChartId", "dbo.ObservationCharts", "Id");
            AddForeignKey("dbo.ObservationChartPulseBloodPressures", "ObservationChartId", "dbo.ObservationCharts", "Id");
        }
    }
}
