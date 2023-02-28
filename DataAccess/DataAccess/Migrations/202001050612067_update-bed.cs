namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatebed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmergencyTriageRecords", "Bed", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EmergencyTriageRecords", "Bed");
        }
    }
}
