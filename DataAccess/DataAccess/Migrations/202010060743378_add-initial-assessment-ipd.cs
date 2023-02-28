namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addinitialassessmentipd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IPDInitialAssessmentForAdultDatas",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                        Code = c.String(),
                        Value = c.String(),
                        IPDInitialAssessmentForAdultId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IPDInitialAssessmentForAdults", t => t.IPDInitialAssessmentForAdultId)
                .Index(t => t.IPDInitialAssessmentForAdultId);
            
            CreateTable(
                "dbo.IPDInitialAssessmentForAdults",
                c => new
                    {
                        Id = c.Guid(nullable: false),
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
                "dbo.IPDInitialAssessmentForChemotherapyDatas",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                        IPDInitialAssessmentForChemotherapyId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IPDInitialAssessmentForChemotherapies", t => t.IPDInitialAssessmentForChemotherapyId)
                .Index(t => t.IPDInitialAssessmentForChemotherapyId);
            
            CreateTable(
                "dbo.IPDInitialAssessmentForChemotherapies",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                        VisitId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IPDInitialAssessmentForFrailElderlyDatas",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                        IPDInitialAssessmentForFrailElderlyId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IPDInitialAssessmentForFrailElderlies", t => t.IPDInitialAssessmentForFrailElderlyId)
                .Index(t => t.IPDInitialAssessmentForFrailElderlyId);
            
            CreateTable(
                "dbo.IPDInitialAssessmentForFrailElderlies",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.IPDs", "IPDInitialAssessmentForAdultId", c => c.Guid());
            AddColumn("dbo.IPDs", "IPDInitialAssessmentForChemotherapyId", c => c.Guid());
            AddColumn("dbo.IPDs", "IPDInitialAssessmentForFrailElderlyId", c => c.Guid());
            CreateIndex("dbo.IPDs", "IPDInitialAssessmentForAdultId");
            CreateIndex("dbo.IPDs", "IPDInitialAssessmentForChemotherapyId");
            CreateIndex("dbo.IPDs", "IPDInitialAssessmentForFrailElderlyId");
            AddForeignKey("dbo.IPDs", "IPDInitialAssessmentForAdultId", "dbo.IPDInitialAssessmentForAdults", "Id");
            AddForeignKey("dbo.IPDs", "IPDInitialAssessmentForChemotherapyId", "dbo.IPDInitialAssessmentForChemotherapies", "Id");
            AddForeignKey("dbo.IPDs", "IPDInitialAssessmentForFrailElderlyId", "dbo.IPDInitialAssessmentForFrailElderlies", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IPDs", "IPDInitialAssessmentForFrailElderlyId", "dbo.IPDInitialAssessmentForFrailElderlies");
            DropForeignKey("dbo.IPDs", "IPDInitialAssessmentForChemotherapyId", "dbo.IPDInitialAssessmentForChemotherapies");
            DropForeignKey("dbo.IPDs", "IPDInitialAssessmentForAdultId", "dbo.IPDInitialAssessmentForAdults");
            DropForeignKey("dbo.IPDInitialAssessmentForFrailElderlyDatas", "IPDInitialAssessmentForFrailElderlyId", "dbo.IPDInitialAssessmentForFrailElderlies");
            DropForeignKey("dbo.IPDInitialAssessmentForChemotherapyDatas", "IPDInitialAssessmentForChemotherapyId", "dbo.IPDInitialAssessmentForChemotherapies");
            DropForeignKey("dbo.IPDInitialAssessmentForAdultDatas", "IPDInitialAssessmentForAdultId", "dbo.IPDInitialAssessmentForAdults");
            DropIndex("dbo.IPDs", new[] { "IPDInitialAssessmentForFrailElderlyId" });
            DropIndex("dbo.IPDs", new[] { "IPDInitialAssessmentForChemotherapyId" });
            DropIndex("dbo.IPDs", new[] { "IPDInitialAssessmentForAdultId" });
            DropIndex("dbo.IPDInitialAssessmentForFrailElderlyDatas", new[] { "IPDInitialAssessmentForFrailElderlyId" });
            DropIndex("dbo.IPDInitialAssessmentForChemotherapyDatas", new[] { "IPDInitialAssessmentForChemotherapyId" });
            DropIndex("dbo.IPDInitialAssessmentForAdultDatas", new[] { "IPDInitialAssessmentForAdultId" });
            DropColumn("dbo.IPDs", "IPDInitialAssessmentForFrailElderlyId");
            DropColumn("dbo.IPDs", "IPDInitialAssessmentForChemotherapyId");
            DropColumn("dbo.IPDs", "IPDInitialAssessmentForAdultId");
            DropTable("dbo.IPDInitialAssessmentForFrailElderlies");
            DropTable("dbo.IPDInitialAssessmentForFrailElderlyDatas");
            DropTable("dbo.IPDInitialAssessmentForChemotherapies");
            DropTable("dbo.IPDInitialAssessmentForChemotherapyDatas");
            DropTable("dbo.IPDInitialAssessmentForAdults");
            DropTable("dbo.IPDInitialAssessmentForAdultDatas");
        }
    }
}
