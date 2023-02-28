namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateeocprogressnote : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EOCs", "OPDPatientProgressNoteId", c => c.Guid());
            AddColumn("dbo.EOCs", "OPDObservationChartId", c => c.Guid());
            CreateIndex("dbo.EOCs", "OPDPatientProgressNoteId");
            CreateIndex("dbo.EOCs", "OPDObservationChartId");
            AddForeignKey("dbo.EOCs", "OPDObservationChartId", "dbo.OPDObservationCharts", "Id");
            AddForeignKey("dbo.EOCs", "OPDPatientProgressNoteId", "dbo.OPDPatientProgressNotes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EOCs", "OPDPatientProgressNoteId", "dbo.OPDPatientProgressNotes");
            DropForeignKey("dbo.EOCs", "OPDObservationChartId", "dbo.OPDObservationCharts");
            DropIndex("dbo.EOCs", new[] { "OPDObservationChartId" });
            DropIndex("dbo.EOCs", new[] { "OPDPatientProgressNoteId" });
            DropColumn("dbo.EOCs", "OPDObservationChartId");
            DropColumn("dbo.EOCs", "OPDPatientProgressNoteId");
        }
    }
}
