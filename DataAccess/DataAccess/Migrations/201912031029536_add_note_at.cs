namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_note_at : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PatientProgressNoteDatas", "NoteAt", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PatientProgressNoteDatas", "NoteAt");
        }
    }
}
