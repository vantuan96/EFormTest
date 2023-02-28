namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createRoundInfoForPrescriptiontable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PrescriptionNoteModels", "FromDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.PrescriptionNoteModels", "ToDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.PrescriptionNoteModels", "Round", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PrescriptionNoteModels", "Round");
            DropColumn("dbo.PrescriptionNoteModels", "ToDate");
            DropColumn("dbo.PrescriptionNoteModels", "FromDate");
        }
    }
}
