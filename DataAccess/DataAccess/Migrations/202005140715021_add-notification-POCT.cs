namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnotificationPOCT : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EDArterialBloodGasTestDatas",
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
                        EDArterialBloodGasTestId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EDArterialBloodGasTests", t => t.EDArterialBloodGasTestId)
                .Index(t => t.EDArterialBloodGasTestId);
            
            CreateTable(
                "dbo.EDArterialBloodGasTests",
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
                        UseBreathingMachine = c.Boolean(nullable: false),
                        BreathingMode = c.String(),
                        BP = c.String(),
                        Vt = c.String(),
                        F = c.String(),
                        RR = c.String(),
                        FIO2 = c.String(),
                        T = c.String(),
                        ExecutionUser = c.Guid(),
                        ExecutionTime = c.DateTime(),
                        DoctorAccept = c.Guid(),
                        AcceptTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EDChemicalBiologyTestDatas",
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
                        EDChemicalBiologyTestId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EDChemicalBiologyTests", t => t.EDChemicalBiologyTestId)
                .Index(t => t.EDChemicalBiologyTestId);
            
            CreateTable(
                "dbo.EDChemicalBiologyTests",
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
                        ExecutionUser = c.Guid(),
                        ExecutionTime = c.DateTime(),
                        DoctorAccept = c.Guid(),
                        AcceptTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EDPointOfCareTestingMasterDatas",
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
                        Form = c.String(),
                        Order = c.Int(nullable: false),
                        Name = c.String(),
                        ViAge = c.String(),
                        EnAge = c.String(),
                        HigherLimit = c.Single(),
                        LowerLimit = c.Single(),
                        HigherAlert = c.Single(),
                        LowerAlert = c.Single(),
                        Result = c.Single(),
                        Unit = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Notifications",
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
                        Seen = c.Boolean(nullable: false),
                        TimeSeen = c.DateTime(),
                        FromUser = c.String(),
                        ToUser = c.String(),
                        Priority = c.Int(nullable: false),
                        ViMessage = c.String(),
                        EnMessage = c.String(),
                        VisitId = c.Guid(),
                        VisitTypeGroupCode = c.String(),
                        Form = c.String(),
                        SpecialtyId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Specialties", "IsPublish", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EDChemicalBiologyTestDatas", "EDChemicalBiologyTestId", "dbo.EDChemicalBiologyTests");
            DropForeignKey("dbo.EDArterialBloodGasTestDatas", "EDArterialBloodGasTestId", "dbo.EDArterialBloodGasTests");
            DropIndex("dbo.EDChemicalBiologyTestDatas", new[] { "EDChemicalBiologyTestId" });
            DropIndex("dbo.EDArterialBloodGasTestDatas", new[] { "EDArterialBloodGasTestId" });
            DropColumn("dbo.Specialties", "IsPublish");
            DropTable("dbo.Notifications");
            DropTable("dbo.EDPointOfCareTestingMasterDatas");
            DropTable("dbo.EDChemicalBiologyTests");
            DropTable("dbo.EDChemicalBiologyTestDatas");
            DropTable("dbo.EDArterialBloodGasTests");
            DropTable("dbo.EDArterialBloodGasTestDatas");
        }
    }
}
