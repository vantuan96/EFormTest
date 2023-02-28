namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateammiteddate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EDs", "AdmittedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.EmergencyTriageRecords", "TriageDateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EmergencyTriageRecords", "TriageDateTime", c => c.DateTime());
            AlterColumn("dbo.EDs", "AdmittedDate", c => c.DateTime());
        }
    }
}
