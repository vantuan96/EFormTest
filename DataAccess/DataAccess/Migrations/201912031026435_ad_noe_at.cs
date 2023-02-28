namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ad_noe_at : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ObservationChartDatas", "NoteAt", c => c.DateTime());
            AddColumn("dbo.ObservationChartPulseBloodPressures", "NoteAt", c => c.DateTime());
            AddColumn("dbo.ObservationChartTemperatures", "NoteAt", c => c.DateTime());
            AddColumn("dbo.PatientProgressNotes", "NoteAt", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PatientProgressNotes", "NoteAt");
            DropColumn("dbo.ObservationChartTemperatures", "NoteAt");
            DropColumn("dbo.ObservationChartPulseBloodPressures", "NoteAt");
            DropColumn("dbo.ObservationChartDatas", "NoteAt");
        }
    }
}
