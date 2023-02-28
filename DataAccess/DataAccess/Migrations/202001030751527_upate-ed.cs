namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upateed : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.EDs", name: "SpongeSharpsAndInstrumentsCountsSheet_Id", newName: "SpongeSharpsAndInstrumentsCountsSheetId");
            RenameIndex(table: "dbo.EDs", name: "IX_SpongeSharpsAndInstrumentsCountsSheet_Id", newName: "IX_SpongeSharpsAndInstrumentsCountsSheetId");
            AddColumn("dbo.PreOperativeProcedureHandoverChecklistDatas", "Code", c => c.String());
            AddColumn("dbo.PreOperativeProcedureHandoverChecklistDatas", "Value", c => c.String());
            AddColumn("dbo.PreOperativeProcedureHandoverChecklistDatas", "EnValue", c => c.String());
            AddColumn("dbo.SpongeSharpsAndInstrumentsCountsSheetDatas", "Code", c => c.String());
            AddColumn("dbo.SpongeSharpsAndInstrumentsCountsSheetDatas", "Value", c => c.String());
            AddColumn("dbo.SpongeSharpsAndInstrumentsCountsSheetDatas", "EnValue", c => c.String());
            AddColumn("dbo.SpongeSharpsAndInstrumentsCountsSheets", "DateTimeSheet", c => c.DateTime());
            AddColumn("dbo.SpongeSharpsAndInstrumentsCountsSheets", "ScrubNurse", c => c.String());
            AddColumn("dbo.SpongeSharpsAndInstrumentsCountsSheets", "CirculatingNurse", c => c.String());
            AddColumn("dbo.SpongeSharpsAndInstrumentsCountsSheets", "Surgeon", c => c.String());
            DropColumn("dbo.SpongeSharpsAndInstrumentsCountsSheetDatas", "DateTimeSheet");
            DropColumn("dbo.SpongeSharpsAndInstrumentsCountsSheetDatas", "ScrubNurse");
            DropColumn("dbo.SpongeSharpsAndInstrumentsCountsSheetDatas", "CirculatingNurse");
            DropColumn("dbo.SpongeSharpsAndInstrumentsCountsSheetDatas", "Surgeon");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SpongeSharpsAndInstrumentsCountsSheetDatas", "Surgeon", c => c.String());
            AddColumn("dbo.SpongeSharpsAndInstrumentsCountsSheetDatas", "CirculatingNurse", c => c.String());
            AddColumn("dbo.SpongeSharpsAndInstrumentsCountsSheetDatas", "ScrubNurse", c => c.String());
            AddColumn("dbo.SpongeSharpsAndInstrumentsCountsSheetDatas", "DateTimeSheet", c => c.DateTime());
            DropColumn("dbo.SpongeSharpsAndInstrumentsCountsSheets", "Surgeon");
            DropColumn("dbo.SpongeSharpsAndInstrumentsCountsSheets", "CirculatingNurse");
            DropColumn("dbo.SpongeSharpsAndInstrumentsCountsSheets", "ScrubNurse");
            DropColumn("dbo.SpongeSharpsAndInstrumentsCountsSheets", "DateTimeSheet");
            DropColumn("dbo.SpongeSharpsAndInstrumentsCountsSheetDatas", "EnValue");
            DropColumn("dbo.SpongeSharpsAndInstrumentsCountsSheetDatas", "Value");
            DropColumn("dbo.SpongeSharpsAndInstrumentsCountsSheetDatas", "Code");
            DropColumn("dbo.PreOperativeProcedureHandoverChecklistDatas", "EnValue");
            DropColumn("dbo.PreOperativeProcedureHandoverChecklistDatas", "Value");
            DropColumn("dbo.PreOperativeProcedureHandoverChecklistDatas", "Code");
            RenameIndex(table: "dbo.EDs", name: "IX_SpongeSharpsAndInstrumentsCountsSheetId", newName: "IX_SpongeSharpsAndInstrumentsCountsSheet_Id");
            RenameColumn(table: "dbo.EDs", name: "SpongeSharpsAndInstrumentsCountsSheetId", newName: "SpongeSharpsAndInstrumentsCountsSheet_Id");
        }
    }
}
