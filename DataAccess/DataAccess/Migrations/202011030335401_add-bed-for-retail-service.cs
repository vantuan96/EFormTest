namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addbedforretailservice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EDAssessmentForRetailServicePatients", "Bed", c => c.String());
            AddColumn("dbo.EDs", "Bed", c => c.String());
            AddColumn("dbo.IPDs", "Reason", c => c.String());
            AddColumn("dbo.IPDs", "Bed", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.IPDs", "Bed");
            DropColumn("dbo.IPDs", "Reason");
            DropColumn("dbo.EDs", "Bed");
            DropColumn("dbo.EDAssessmentForRetailServicePatients", "Bed");
        }
    }
}
