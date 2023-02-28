namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatesprint22 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IPDGlamorganDatas",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Code = c.String(),
                        Value = c.String(),
                        FormCode = c.String(),
                        IPDGlamorganId = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IPDGlamorgans", t => t.IPDGlamorganId)
                .Index(t => t.IPDGlamorganId);
            
            CreateTable(
                "dbo.IPDGlamorgans",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VisitId = c.Guid(),
                        NurseConfirmId = c.Guid(),
                        NurseConfirmAt = c.DateTime(),
                        Total = c.String(),
                        Level = c.String(),
                        Intervention = c.String(),
                        StartingAssessment = c.DateTime(),
                        Number = c.Int(nullable: false),
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
                "dbo.IPDMedicalRecordExtenstions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        VisitId = c.Guid(),
                        DoctorConfirmId = c.Guid(),
                        DoctorConfirmAt = c.DateTime(),
                        FormCode = c.String(),
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
                "dbo.IPDNeonatalObservationCharts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VisitId = c.Guid(),
                        TransactionDate = c.DateTime(nullable: false),
                        Note = c.String(),
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
                "dbo.IPDVitalSignForPediatrics",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BloodVesselPews = c.String(),
                        BreathingPews = c.String(),
                        HypothermiaPews = c.String(),
                        TotalPews = c.String(),
                        TransactionDate = c.DateTime(),
                        FormType = c.String(),
                        VisitId = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IPDGlamorganDatas", "IPDGlamorganId", "dbo.IPDGlamorgans");
            DropIndex("dbo.IPDGlamorganDatas", new[] { "IPDGlamorganId" });
            DropTable("dbo.IPDVitalSignForPediatrics");
            DropTable("dbo.IPDNeonatalObservationCharts");
            DropTable("dbo.IPDMedicalRecordExtenstions");
            DropTable("dbo.IPDGlamorgans");
            DropTable("dbo.IPDGlamorganDatas");
        }
    }
}
