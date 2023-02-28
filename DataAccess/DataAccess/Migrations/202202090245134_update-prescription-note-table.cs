namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateprescriptionnotetable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PrescriptionNoteModels", "FromDate");
            DropColumn("dbo.PrescriptionNoteModels", "ToDate");
            DropColumn("dbo.PrescriptionNoteModels", "Round");
        }
        
        public override void Down()
        {

        }
    }
}
