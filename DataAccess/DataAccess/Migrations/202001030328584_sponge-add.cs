namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class spongeadd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SpongeSharpsAndInstrumentsCountsSheetDatas",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTime(),
                        DeletedBy = c.String(),
                        DateTimeSheet = c.DateTime(),
                        ScrubNurse = c.String(),
                        CirculatingNurse = c.String(),
                        Surgeon = c.String(),
                        SpongeSharpsAndInstrumentsCountsSheetId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SpongeSharpsAndInstrumentsCountsSheets", t => t.SpongeSharpsAndInstrumentsCountsSheetId)
                .Index(t => t.SpongeSharpsAndInstrumentsCountsSheetId);
            
            CreateTable(
                "dbo.SpongeSharpsAndInstrumentsCountsSheets",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTime(),
                        DeletedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.EDs", "SpongeSharpsAndInstrumentsCountsSheet_Id", c => c.Guid());
            AddColumn("dbo.PreOperativeProcedureHandoverChecklists", "DateOfOperation", c => c.DateTime());
            CreateIndex("dbo.EDs", "SpongeSharpsAndInstrumentsCountsSheet_Id");
            AddForeignKey("dbo.EDs", "SpongeSharpsAndInstrumentsCountsSheet_Id", "dbo.SpongeSharpsAndInstrumentsCountsSheets", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SpongeSharpsAndInstrumentsCountsSheetDatas", "SpongeSharpsAndInstrumentsCountsSheetId", "dbo.SpongeSharpsAndInstrumentsCountsSheets");
            DropForeignKey("dbo.EDs", "SpongeSharpsAndInstrumentsCountsSheet_Id", "dbo.SpongeSharpsAndInstrumentsCountsSheets");
            DropIndex("dbo.SpongeSharpsAndInstrumentsCountsSheetDatas", new[] { "SpongeSharpsAndInstrumentsCountsSheetId" });
            DropIndex("dbo.EDs", new[] { "SpongeSharpsAndInstrumentsCountsSheet_Id" });
            DropColumn("dbo.PreOperativeProcedureHandoverChecklists", "DateOfOperation");
            DropColumn("dbo.EDs", "SpongeSharpsAndInstrumentsCountsSheet_Id");
            DropTable("dbo.SpongeSharpsAndInstrumentsCountsSheets");
            DropTable("dbo.SpongeSharpsAndInstrumentsCountsSheetDatas");
        }
    }
}
