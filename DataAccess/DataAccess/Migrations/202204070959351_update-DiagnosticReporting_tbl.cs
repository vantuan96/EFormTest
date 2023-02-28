namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDiagnosticReporting_tbl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DiagnosticReportings", "ChargeItemId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DiagnosticReportings", "ChargeItemId");
        }
    }
}
