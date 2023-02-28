namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddrstbl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Charges",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VisitId = c.Guid(),
                        Reason = c.String(),
                        Priority = c.String(),
                        Diagnosis = c.String(),
                        DoctorAD = c.String(),
                        PatientVisitId = c.Guid(),
                        PatientLocationId = c.Guid(),
                        VisitCode = c.String(),
                        VisitType = c.String(),
                        HospitalCode = c.String(),
                        ChargeVisitId = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChargeVisits", t => t.ChargeVisitId)
                .Index(t => t.ChargeVisitId);
            
            CreateTable(
                "dbo.ChargeVisits",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PatientLocationCode = c.String(),
                        VisitGroupType = c.String(),
                        VisitCode = c.String(),
                        AreaName = c.String(),
                        VisitType = c.String(),
                        HospitalCode = c.String(),
                        DoctorAD = c.String(),
                        PatientLocationId = c.Guid(),
                        PatientVisitId = c.Guid(),
                        ActualVisitDate = c.DateTime(),
                        CustomerId = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.ChargeItemMicrobiologies",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        STypeGroupID = c.String(),
                        STypeID = c.String(),
                        Stratified = c.String(),
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
                "dbo.ChargeItemPathologies",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PathologyType = c.String(),
                        SurgeryMethod = c.String(),
                        SiteOfSpecimen = c.String(),
                        CollectionTime = c.String(),
                        StoreSpecimenWithSolution = c.String(),
                        TimeOfStore = c.String(),
                        SpecimenOrientation = c.String(),
                        ClinicalHistoryAndLabTests = c.String(),
                        Treatment = c.String(),
                        GrosDescription = c.String(),
                        PreviousResults = c.String(),
                        ClinicalDiagnosis = c.String(),
                        LatestMenstrualPeriod = c.String(),
                        TheLastDayOfLatestMenstrualPeriod = c.String(),
                        PostMenopause = c.String(),
                        CervicalCytologyTestHistoryAndTreatmentBefore = c.String(),
                        HistoryOfSquamousCellCarcinoma = c.String(),
                        BirthControlMethod = c.String(),
                        HormoneTreatment = c.String(),
                        UterusRemoval = c.String(),
                        RadiationTheorapy = c.String(),
                        PostMenopauseBleeding = c.String(),
                        Pregnant = c.String(),
                        Postpartum = c.String(),
                        BirthControlPills = c.String(),
                        Breastfeeding = c.String(),
                        GynecologicalCytologyType = c.String(),
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
                "dbo.ChargeItems",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        HospitalCode = c.String(),
                        PatientLocationCode = c.String(),
                        VisitGroupType = c.String(),
                        CustomerId = c.Guid(nullable: false),
                        PatientVisitId = c.Guid(nullable: false),
                        VisitCode = c.String(),
                        VisitType = c.String(),
                        ChargeId = c.Guid(nullable: false),
                        ItemId = c.Guid(nullable: false),
                        ServiceCode = c.String(),
                        ServiceId = c.Guid(nullable: false),
                        ItemType = c.Int(nullable: false),
                        ServiceType = c.Int(nullable: false),
                        ChargeSessionId = c.Guid(),
                        ChargeDetailId = c.Guid(),
                        Filler = c.String(),
                        FillerGroup = c.String(),
                        Quantity = c.String(),
                        PatientId = c.String(),
                        CostCentreId = c.Guid(),
                        PatientLocationId = c.Guid(nullable: false),
                        PlacerOrderableId = c.Guid(),
                        ServiceDepartmentId = c.Guid(),
                        DoctorAD = c.String(),
                        Priority = c.String(),
                        Comments = c.String(),
                        Instructions = c.String(),
                        InitialDiagnosis = c.String(),
                        Reason = c.String(),
                        PlacerIdentifyNumber = c.Int(nullable: false, identity: true),
                        AdditionalInformation = c.String(),
                        Status = c.String(),
                        CancelComment = c.String(),
                        ChargeItemType = c.String(),
                        FailedReason = c.String(),
                        CancelFailedReason = c.String(),
                        ChargeItemPathologyId = c.Guid(),
                        ChargeItemMicrobiologyId = c.Guid(),
                        RadiologyProcedureId = c.Guid(),
                        PaymentStatus = c.String(),
                        RadiologyScheduledStatus = c.String(),
                        PlacerOrderStatus = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChargeItemMicrobiologies", t => t.ChargeItemMicrobiologyId)
                .ForeignKey("dbo.ChargeItemPathologies", t => t.ChargeItemPathologyId)
                .ForeignKey("dbo.RadiologyProcedurePlanRefs", t => t.RadiologyProcedureId)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: true)
                .Index(t => t.ServiceId)
                .Index(t => t.ChargeItemPathologyId)
                .Index(t => t.ChargeItemMicrobiologyId)
                .Index(t => t.RadiologyProcedureId);
            
            CreateTable(
                "dbo.RadiologyProcedurePlanRefs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RadiologyProcedurePlanRid = c.Guid(nullable: false),
                        ShortCode = c.String(),
                        RadiologyProcedureNameE = c.String(),
                        RadiologyProcedureNameL = c.String(),
                        ActiveStatus = c.String(),
                        ServiceCategoryCode = c.String(),
                        ServiceCategoryNameL = c.String(),
                        ServiceCategoryNameE = c.String(),
                        DicomModality = c.String(),
                        LuUpdated = c.DateTime(nullable: false),
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
                "dbo.Services",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        HISId = c.Guid(nullable: false),
                        ViName = c.String(),
                        EnName = c.String(),
                        Code = c.String(maxLength: 50, unicode: false),
                        HISCode = c.Int(nullable: false),
                        HISLastUpdated = c.DateTime(nullable: false),
                        ServiceGroupId = c.Guid(),
                        IsActive = c.Boolean(nullable: false),
                        Type = c.String(maxLength: 50, unicode: false),
                        CombinedName = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ServiceGroups", t => t.ServiceGroupId)
                .Index(t => t.Code)
                .Index(t => t.ServiceGroupId);
            
            CreateTable(
                "dbo.ServiceGroups",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        HISId = c.Guid(nullable: false),
                        ViName = c.String(),
                        EnName = c.String(),
                        Code = c.String(maxLength: 50, unicode: false),
                        HISCode = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Code);
            
            CreateTable(
                "dbo.CpoeOrderables",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CpoeOrderableId = c.Guid(nullable: false),
                        PhGenericDrugId = c.Guid(),
                        ServiceCategoryRcd = c.String(),
                        LabOrderableRid = c.Guid(),
                        GenericOrderableServiceCodeRid = c.Guid(),
                        RadiologyProcedurePlanRid = c.Guid(),
                        PhPharmacyProductId = c.Guid(),
                        PackageItemId = c.Guid(),
                        CpoeOrderableTypeRcd = c.String(),
                        OverrideNameE = c.String(),
                        OverrideNameL = c.String(),
                        LuUserId = c.Guid(),
                        SeqNum = c.String(),
                        LuUpdated = c.DateTime(nullable: false),
                        EffectiveFromDateTime = c.DateTime(nullable: false),
                        EffectiveToDateTime = c.DateTime(),
                        Comments = c.String(),
                        FillerNameE = c.String(),
                        FillerNameL = c.String(),
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
                "dbo.LabOrderableRefs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LabOrderableRid = c.Guid(nullable: false),
                        ItemId = c.Guid(),
                        LabSpecialProcessingGroupRcd = c.String(),
                        LabOrderableCode = c.String(),
                        ServiceCategoryRcd = c.String(),
                        NameE = c.String(),
                        NameL = c.String(),
                        SeqNum = c.String(),
                        ActiveStatus = c.String(),
                        LuUserId = c.Guid(),
                        LuUpdated = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.SystemConfigs", "LastUpdatedOHService", c => c.DateTime());
            AddColumn("dbo.SystemConfigs", "TypeConfig", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChargeItems", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.Services", "ServiceGroupId", "dbo.ServiceGroups");
            DropForeignKey("dbo.ChargeItems", "RadiologyProcedureId", "dbo.RadiologyProcedurePlanRefs");
            DropForeignKey("dbo.ChargeItems", "ChargeItemPathologyId", "dbo.ChargeItemPathologies");
            DropForeignKey("dbo.ChargeItems", "ChargeItemMicrobiologyId", "dbo.ChargeItemMicrobiologies");
            DropForeignKey("dbo.ChargeVisits", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Charges", "ChargeVisitId", "dbo.ChargeVisits");
            DropIndex("dbo.ServiceGroups", new[] { "Code" });
            DropIndex("dbo.Services", new[] { "ServiceGroupId" });
            DropIndex("dbo.Services", new[] { "Code" });
            DropIndex("dbo.ChargeItems", new[] { "RadiologyProcedureId" });
            DropIndex("dbo.ChargeItems", new[] { "ChargeItemMicrobiologyId" });
            DropIndex("dbo.ChargeItems", new[] { "ChargeItemPathologyId" });
            DropIndex("dbo.ChargeItems", new[] { "ServiceId" });
            DropIndex("dbo.ChargeVisits", new[] { "CustomerId" });
            DropIndex("dbo.Charges", new[] { "ChargeVisitId" });
            DropColumn("dbo.SystemConfigs", "TypeConfig");
            DropColumn("dbo.SystemConfigs", "LastUpdatedOHService");
            DropTable("dbo.LabOrderableRefs");
            DropTable("dbo.CpoeOrderables");
            DropTable("dbo.ServiceGroups");
            DropTable("dbo.Services");
            DropTable("dbo.RadiologyProcedurePlanRefs");
            DropTable("dbo.ChargeItems");
            DropTable("dbo.ChargeItemPathologies");
            DropTable("dbo.ChargeItemMicrobiologies");
            DropTable("dbo.ChargeVisits");
            DropTable("dbo.Charges");
        }
    }
}
