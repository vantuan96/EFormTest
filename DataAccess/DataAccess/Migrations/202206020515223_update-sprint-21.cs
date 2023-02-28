namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatesprint21 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EDs", "EDConsultationDrugWithAnAsteriskMarkId", "dbo.EDConsultationDrugWithAnAsteriskMarks");
            DropForeignKey("dbo.IPDs", "IPDConsultationDrugWithAnAsteriskMarkId", "dbo.IPDConsultationDrugWithAnAsteriskMarks");
            DropIndex("dbo.EDs", new[] { "EDConsultationDrugWithAnAsteriskMarkId" });
            DropIndex("dbo.IPDs", new[] { "IPDConsultationDrugWithAnAsteriskMarkId" });
            CreateTable(
                "dbo.ICD10Visit",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VisitId = c.Guid(nullable: false),
                        FormId = c.Guid(),
                        Type = c.String(),
                        MasterDataCode = c.String(),
                        Code = c.String(),
                        Name = c.String(),
                        EnName = c.String(),
                        Diagnosis = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IPDPainRecords",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VisitId = c.Guid(),
                        DataType = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PreAnesthesiaConsultations",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VisitId = c.Guid(nullable: false),
                        VisitTypeGroupCode = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ChargeItems", "Price", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.EDConsultationDrugWithAnAsteriskMarks", "VisitId", c => c.Guid());
            AddColumn("dbo.Orders", "FormId", c => c.Guid());
            AddColumn("dbo.EDHandOverCheckLists", "IsUseHandOverCheckList", c => c.Boolean(nullable: false));
            AddColumn("dbo.EOCHandOverCheckLists", "IsUseHandOverCheckList", c => c.Boolean(nullable: false));
            AddColumn("dbo.HighlyRestrictedAntimicrobialConsults", "VisitId", c => c.Guid(nullable: false));
            AddColumn("dbo.IPDConsultationDrugWithAnAsteriskMarks", "VisitId", c => c.Guid());
            AddColumn("dbo.IPDFallRiskAssessmentForAdults", "FormType", c => c.String());
            AddColumn("dbo.OPDHandOverCheckLists", "IsUseHandOverCheckList", c => c.Boolean(nullable: false));
            CreateIndex("dbo.EDConsultationDrugWithAnAsteriskMarks", "VisitId");
            CreateIndex("dbo.IPDConsultationDrugWithAnAsteriskMarks", "VisitId");
            AddForeignKey("dbo.EDConsultationDrugWithAnAsteriskMarks", "VisitId", "dbo.EDs", "Id");
            AddForeignKey("dbo.IPDConsultationDrugWithAnAsteriskMarks", "VisitId", "dbo.IPDs", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IPDConsultationDrugWithAnAsteriskMarks", "VisitId", "dbo.IPDs");
            DropForeignKey("dbo.EDConsultationDrugWithAnAsteriskMarks", "VisitId", "dbo.EDs");
            DropIndex("dbo.IPDConsultationDrugWithAnAsteriskMarks", new[] { "VisitId" });
            DropIndex("dbo.EDConsultationDrugWithAnAsteriskMarks", new[] { "VisitId" });
            DropColumn("dbo.OPDHandOverCheckLists", "IsUseHandOverCheckList");
            DropColumn("dbo.IPDFallRiskAssessmentForAdults", "FormType");
            DropColumn("dbo.IPDConsultationDrugWithAnAsteriskMarks", "VisitId");
            DropColumn("dbo.HighlyRestrictedAntimicrobialConsults", "VisitId");
            DropColumn("dbo.EOCHandOverCheckLists", "IsUseHandOverCheckList");
            DropColumn("dbo.EDHandOverCheckLists", "IsUseHandOverCheckList");
            DropColumn("dbo.Orders", "FormId");
            DropColumn("dbo.EDConsultationDrugWithAnAsteriskMarks", "VisitId");
            DropColumn("dbo.ChargeItems", "Price");
            DropTable("dbo.PreAnesthesiaConsultations");
            DropTable("dbo.IPDPainRecords");
            DropTable("dbo.ICD10Visit");
            CreateIndex("dbo.IPDs", "IPDConsultationDrugWithAnAsteriskMarkId");
            CreateIndex("dbo.EDs", "EDConsultationDrugWithAnAsteriskMarkId");
            AddForeignKey("dbo.IPDs", "IPDConsultationDrugWithAnAsteriskMarkId", "dbo.IPDConsultationDrugWithAnAsteriskMarks", "Id");
            AddForeignKey("dbo.EDs", "EDConsultationDrugWithAnAsteriskMarkId", "dbo.EDConsultationDrugWithAnAsteriskMarks", "Id");
        }
    }
}
