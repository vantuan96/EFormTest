namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateIPDmortalityreport : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.EDMortalityReports", newName: "EIOMortalityReports");
            RenameTable(name: "dbo.EDMortalityReportMembers", newName: "EIOMortalityReportMembers");
            DropForeignKey("dbo.EDs", "EDMortalityReportId", "dbo.EDMortalityReports");
            DropIndex("dbo.EDs", new[] { "EDMortalityReportId" });
            AddColumn("dbo.EIOMortalityReports", "VisitId", c => c.Guid());
            AddColumn("dbo.EIOMortalityReports", "VisitTypeGroupCode", c => c.String());
            DropColumn("dbo.EDs", "EDMortalityReportId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EDs", "EDMortalityReportId", c => c.Guid());
            DropColumn("dbo.EIOMortalityReports", "VisitTypeGroupCode");
            DropColumn("dbo.EIOMortalityReports", "VisitId");
            CreateIndex("dbo.EDs", "EDMortalityReportId");
            AddForeignKey("dbo.EDs", "EDMortalityReportId", "dbo.EDMortalityReports", "Id");
            RenameTable(name: "dbo.EIOMortalityReportMembers", newName: "EDMortalityReportMembers");
            RenameTable(name: "dbo.EIOMortalityReports", newName: "EDMortalityReports");
        }
    }
}
