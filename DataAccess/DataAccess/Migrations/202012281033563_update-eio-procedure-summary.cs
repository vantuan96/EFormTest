namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateeioproceduresummary : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.OPDProcedureSummaries", newName: "EIOProcedureSummaries");
            RenameTable(name: "dbo.OPDProcedureSummaryDatas", newName: "EIOProcedureSummaryDatas");
            RenameColumn(table: "dbo.EIOProcedureSummaryDatas", name: "OPDProcedureSummaryId", newName: "EIOProcedureSummaryId");
            RenameIndex(table: "dbo.EIOProcedureSummaryDatas", name: "IX_OPDProcedureSummaryId", newName: "IX_EIOProcedureSummaryId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.EIOProcedureSummaryDatas", name: "IX_EIOProcedureSummaryId", newName: "IX_OPDProcedureSummaryId");
            RenameColumn(table: "dbo.EIOProcedureSummaryDatas", name: "EIOProcedureSummaryId", newName: "OPDProcedureSummaryId");
            RenameTable(name: "dbo.EIOProcedureSummaryDatas", newName: "OPDProcedureSummaryDatas");
            RenameTable(name: "dbo.EIOProcedureSummaries", newName: "OPDProcedureSummaries");
        }
    }
}
