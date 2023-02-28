namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateproceduresummary : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.OPDProcedureSumarryDatas", newName: "OPDProcedureSummaryDatas");
            RenameTable(name: "dbo.OPDProcedureSumarries", newName: "OPDProcedureSummaries");
            RenameColumn(table: "dbo.OPDProcedureSummaryDatas", name: "OPDProcedureSumarryId", newName: "OPDProcedureSummaryId");
            RenameIndex(table: "dbo.OPDProcedureSummaryDatas", name: "IX_OPDProcedureSumarryId", newName: "IX_OPDProcedureSummaryId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.OPDProcedureSummaryDatas", name: "IX_OPDProcedureSummaryId", newName: "IX_OPDProcedureSumarryId");
            RenameColumn(table: "dbo.OPDProcedureSummaryDatas", name: "OPDProcedureSummaryId", newName: "OPDProcedureSumarryId");
            RenameTable(name: "dbo.OPDProcedureSummaries", newName: "OPDProcedureSumarries");
            RenameTable(name: "dbo.OPDProcedureSummaryDatas", newName: "OPDProcedureSumarryDatas");
        }
    }
}
