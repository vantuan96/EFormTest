namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addipddischargechecklisttables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IPDDischargePreparationChecklistDatas",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Code = c.String(),
                        Value = c.String(),
                        IPDDischargePreparationChecklistId = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IPDDischargePreparationChecklists", t => t.IPDDischargePreparationChecklistId)
                .Index(t => t.IPDDischargePreparationChecklistId);
            
            CreateTable(
                "dbo.IPDDischargePreparationChecklists",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Type = c.String(),
                        Version = c.Int(nullable: false),
                        VisitId = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IPDs", t => t.VisitId)
                .Index(t => t.VisitId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IPDDischargePreparationChecklists", "VisitId", "dbo.IPDs");
            DropForeignKey("dbo.IPDDischargePreparationChecklistDatas", "IPDDischargePreparationChecklistId", "dbo.IPDDischargePreparationChecklists");
            DropIndex("dbo.IPDDischargePreparationChecklists", new[] { "VisitId" });
            DropIndex("dbo.IPDDischargePreparationChecklistDatas", new[] { "IPDDischargePreparationChecklistId" });
            DropTable("dbo.IPDDischargePreparationChecklists");
            DropTable("dbo.IPDDischargePreparationChecklistDatas");
        }
    }
}
