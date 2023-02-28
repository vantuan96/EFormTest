namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createopdobservationchart : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OPDObservationChartDatas",
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
                        SysBP = c.String(),
                        DiaBP = c.String(),
                        Pulse = c.String(),
                        Temperature = c.String(),
                        Resp = c.String(),
                        SpO2 = c.String(),
                        RestPainScore = c.String(),
                        MovePainScore = c.String(),
                        NoteAt = c.DateTime(),
                        Other = c.String(),
                        ObservationChartId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OPDObservationCharts", t => t.ObservationChartId)
                .Index(t => t.ObservationChartId);
            
            CreateTable(
                "dbo.OPDObservationCharts",
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
            
            AddColumn("dbo.OPDs", "OPDObservationChartId", c => c.Guid());
            CreateIndex("dbo.OPDs", "OPDObservationChartId");
            AddForeignKey("dbo.OPDs", "OPDObservationChartId", "dbo.OPDObservationCharts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OPDs", "OPDObservationChartId", "dbo.OPDObservationCharts");
            DropForeignKey("dbo.OPDObservationChartDatas", "ObservationChartId", "dbo.OPDObservationCharts");
            DropIndex("dbo.OPDs", new[] { "OPDObservationChartId" });
            DropIndex("dbo.OPDObservationChartDatas", new[] { "ObservationChartId" });
            DropColumn("dbo.OPDs", "OPDObservationChartId");
            DropTable("dbo.OPDObservationCharts");
            DropTable("dbo.OPDObservationChartDatas");
        }
    }
}
