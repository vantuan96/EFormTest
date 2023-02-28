namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initHighlyResAnitimicrobialConsult : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HighlyRestrictedAntimicrobialConsults",
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
                        VisitTypeGroupCode = c.String(),
                        DiagnosisOfInfection = c.String(),
                        DiagnosisOfInfectionOther = c.String(),
                        ClinicalSymtoms = c.String(),
                        CurrentAntimicrobials = c.String(),
                        IndicationsOfHighlyRestrictedAntimicrobials = c.String(),
                        IndicationsOfHighlyRestrictedAntimicrobialsOther = c.String(),
                        IsAllergy = c.Boolean(nullable: false),
                        Allergy = c.String(),
                        KindOfAllergy = c.String(),
                        ConfirmDate = c.DateTime(),
                        ConfirmDoctorId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.ConfirmDoctorId)
                .Index(t => t.ConfirmDoctorId);
            
            CreateTable(
                "dbo.HighlyRestrictedAntimicrobialConsultAntimicrobialOrders",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Antimicrobial = c.String(),
                        Dose = c.String(),
                        Frequency = c.String(),
                        Duration = c.String(),
                        HighlyRestrictedAntimicrobialConsultId = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HighlyRestrictedAntimicrobialConsults", t => t.HighlyRestrictedAntimicrobialConsultId)
                .Index(t => t.HighlyRestrictedAntimicrobialConsultId);
            
            CreateTable(
                "dbo.HighlyRestrictedAntimicrobialConsultMicrobiologicalResults",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Date = c.String(),
                        Culture = c.String(),
                        Specimen = c.String(),
                        Others = c.String(),
                        HighlyRestrictedAntimicrobialConsultId = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HighlyRestrictedAntimicrobialConsults", t => t.HighlyRestrictedAntimicrobialConsultId)
                .Index(t => t.HighlyRestrictedAntimicrobialConsultId);
            
            AddColumn("dbo.EDs", "HighlyRestrictedAntimicrobialConsultId", c => c.Guid());
            AddColumn("dbo.IPDs", "HighlyRestrictedAntimicrobialConsultId", c => c.Guid());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HighlyRestrictedAntimicrobialConsultMicrobiologicalResults", "HighlyRestrictedAntimicrobialConsultId", "dbo.HighlyRestrictedAntimicrobialConsults");
            DropForeignKey("dbo.HighlyRestrictedAntimicrobialConsultAntimicrobialOrders", "HighlyRestrictedAntimicrobialConsultId", "dbo.HighlyRestrictedAntimicrobialConsults");
            DropForeignKey("dbo.HighlyRestrictedAntimicrobialConsults", "ConfirmDoctorId", "dbo.Users");
            DropIndex("dbo.HighlyRestrictedAntimicrobialConsultMicrobiologicalResults", new[] { "HighlyRestrictedAntimicrobialConsultId" });
            DropIndex("dbo.HighlyRestrictedAntimicrobialConsultAntimicrobialOrders", new[] { "HighlyRestrictedAntimicrobialConsultId" });
            DropIndex("dbo.HighlyRestrictedAntimicrobialConsults", new[] { "ConfirmDoctorId" });
            DropColumn("dbo.IPDs", "HighlyRestrictedAntimicrobialConsultId");
            DropColumn("dbo.EDs", "HighlyRestrictedAntimicrobialConsultId");
            DropTable("dbo.HighlyRestrictedAntimicrobialConsultMicrobiologicalResults");
            DropTable("dbo.HighlyRestrictedAntimicrobialConsultAntimicrobialOrders");
            DropTable("dbo.HighlyRestrictedAntimicrobialConsults");
        }
    }
}
