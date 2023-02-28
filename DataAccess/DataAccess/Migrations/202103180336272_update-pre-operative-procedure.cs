namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatepreoperativeprocedure : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.EDPreOperativeProcedureHandoverChecklistDatas", newName: "EIOPreOperativeProcedureHandoverChecklistDatas");
            RenameTable(name: "dbo.EDPreOperativeProcedureHandoverChecklists", newName: "EIOPreOperativeProcedureHandoverChecklists");
            RenameTable(name: "dbo.EDSpongeSharpsAndInstrumentsCountsSheets", newName: "EIOSpongeSharpsAndInstrumentsCountsSheets");
            RenameTable(name: "dbo.EDSpongeSharpsAndInstrumentsCountsSheetDatas", newName: "EIOSpongeSharpsAndInstrumentsCountsSheetDatas");
            AddColumn("dbo.EIOPreOperativeProcedureHandoverChecklists", "VisitId", c => c.Guid());
            AddColumn("dbo.EIOPreOperativeProcedureHandoverChecklists", "VisitTypeGroupCode", c => c.String());
            AddColumn("dbo.EIOSpongeSharpsAndInstrumentsCountsSheets", "VisitId", c => c.Guid());
            AddColumn("dbo.EIOSpongeSharpsAndInstrumentsCountsSheets", "VisitTypeGroupCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EIOSpongeSharpsAndInstrumentsCountsSheets", "VisitTypeGroupCode");
            DropColumn("dbo.EIOSpongeSharpsAndInstrumentsCountsSheets", "VisitId");
            DropColumn("dbo.EIOPreOperativeProcedureHandoverChecklists", "VisitTypeGroupCode");
            DropColumn("dbo.EIOPreOperativeProcedureHandoverChecklists", "VisitId");
            RenameTable(name: "dbo.EIOSpongeSharpsAndInstrumentsCountsSheetDatas", newName: "EDSpongeSharpsAndInstrumentsCountsSheetDatas");
            RenameTable(name: "dbo.EIOSpongeSharpsAndInstrumentsCountsSheets", newName: "EDSpongeSharpsAndInstrumentsCountsSheets");
            RenameTable(name: "dbo.EIOPreOperativeProcedureHandoverChecklists", newName: "EDPreOperativeProcedureHandoverChecklists");
            RenameTable(name: "dbo.EIOPreOperativeProcedureHandoverChecklistDatas", newName: "EDPreOperativeProcedureHandoverChecklistDatas");
        }
    }
}
