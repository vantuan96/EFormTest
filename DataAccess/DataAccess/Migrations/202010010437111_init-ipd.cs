namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initipd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IPDHandOverCheckListDatas",
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
                        Code = c.String(),
                        Value = c.String(),
                        EnValue = c.String(),
                        HandOverCheckListId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IPDHandOverCheckLists", t => t.HandOverCheckListId)
                .Index(t => t.HandOverCheckListId);
            
            CreateTable(
                "dbo.IPDHandOverCheckLists",
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
                        ReasonForTransfer = c.String(),
                        HandOverTimePhysician = c.DateTime(),
                        HandOverPhysicianId = c.Guid(),
                        HandOverUnitPhysicianId = c.Guid(),
                        ReceivingPhysicianId = c.Guid(),
                        ReceivingUnitPhysicianId = c.Guid(),
                        IsAcceptPhysician = c.Boolean(nullable: false),
                        HandOverTimeNurse = c.DateTime(),
                        HandOverNurseId = c.Guid(),
                        HandOverUnitNurseId = c.Guid(),
                        ReceivingNurseId = c.Guid(),
                        ReceivingUnitNurseId = c.Guid(),
                        IsAcceptNurse = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.HandOverNurseId)
                .ForeignKey("dbo.Users", t => t.HandOverPhysicianId)
                .ForeignKey("dbo.Specialties", t => t.HandOverUnitNurseId)
                .ForeignKey("dbo.Specialties", t => t.HandOverUnitPhysicianId)
                .ForeignKey("dbo.Users", t => t.ReceivingNurseId)
                .ForeignKey("dbo.Users", t => t.ReceivingPhysicianId)
                .ForeignKey("dbo.Specialties", t => t.ReceivingUnitNurseId)
                .ForeignKey("dbo.Specialties", t => t.ReceivingUnitPhysicianId)
                .Index(t => t.HandOverPhysicianId)
                .Index(t => t.HandOverUnitPhysicianId)
                .Index(t => t.ReceivingPhysicianId)
                .Index(t => t.ReceivingUnitPhysicianId)
                .Index(t => t.HandOverNurseId)
                .Index(t => t.HandOverUnitNurseId)
                .Index(t => t.ReceivingNurseId)
                .Index(t => t.ReceivingUnitNurseId);
            
            CreateTable(
                "dbo.IPDs",
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
                        AdmittedDate = c.DateTime(nullable: false),
                        DischargeDate = c.DateTime(),
                        VisitCode = c.String(maxLength: 255, unicode: false),
                        RecordCode = c.String(maxLength: 100, unicode: false),
                        HealthInsuranceNumber = c.String(),
                        StartHealthInsuranceDate = c.DateTime(),
                        ExpireHealthInsuranceDate = c.DateTime(),
                        IsTransfer = c.Boolean(nullable: false),
                        TransferFromId = c.Guid(),
                        PermissionForVisitor = c.Boolean(nullable: false),
                        CustomerId = c.Guid(),
                        EDStatusId = c.Guid(),
                        SiteId = c.Guid(),
                        SpecialtyId = c.Guid(),
                        ClinicId = c.Guid(),
                        PrimaryDoctorId = c.Guid(),
                        PrimaryNurseId = c.Guid(),
                        HandOverCheckListId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clinics", t => t.ClinicId)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .ForeignKey("dbo.EDStatus", t => t.EDStatusId)
                .ForeignKey("dbo.IPDHandOverCheckLists", t => t.HandOverCheckListId)
                .ForeignKey("dbo.Users", t => t.PrimaryDoctorId)
                .ForeignKey("dbo.Users", t => t.PrimaryNurseId)
                .ForeignKey("dbo.Sites", t => t.SiteId)
                .ForeignKey("dbo.Specialties", t => t.SpecialtyId)
                .Index(t => t.VisitCode)
                .Index(t => t.RecordCode)
                .Index(t => t.CustomerId)
                .Index(t => t.EDStatusId)
                .Index(t => t.SiteId)
                .Index(t => t.SpecialtyId)
                .Index(t => t.ClinicId)
                .Index(t => t.PrimaryDoctorId)
                .Index(t => t.PrimaryNurseId)
                .Index(t => t.HandOverCheckListId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IPDs", "SpecialtyId", "dbo.Specialties");
            DropForeignKey("dbo.IPDs", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.IPDs", "PrimaryNurseId", "dbo.Users");
            DropForeignKey("dbo.IPDs", "PrimaryDoctorId", "dbo.Users");
            DropForeignKey("dbo.IPDs", "HandOverCheckListId", "dbo.IPDHandOverCheckLists");
            DropForeignKey("dbo.IPDs", "EDStatusId", "dbo.EDStatus");
            DropForeignKey("dbo.IPDs", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.IPDs", "ClinicId", "dbo.Clinics");
            DropForeignKey("dbo.IPDHandOverCheckLists", "ReceivingUnitPhysicianId", "dbo.Specialties");
            DropForeignKey("dbo.IPDHandOverCheckLists", "ReceivingUnitNurseId", "dbo.Specialties");
            DropForeignKey("dbo.IPDHandOverCheckLists", "ReceivingPhysicianId", "dbo.Users");
            DropForeignKey("dbo.IPDHandOverCheckLists", "ReceivingNurseId", "dbo.Users");
            DropForeignKey("dbo.IPDHandOverCheckLists", "HandOverUnitPhysicianId", "dbo.Specialties");
            DropForeignKey("dbo.IPDHandOverCheckLists", "HandOverUnitNurseId", "dbo.Specialties");
            DropForeignKey("dbo.IPDHandOverCheckLists", "HandOverPhysicianId", "dbo.Users");
            DropForeignKey("dbo.IPDHandOverCheckLists", "HandOverNurseId", "dbo.Users");
            DropForeignKey("dbo.IPDHandOverCheckListDatas", "HandOverCheckListId", "dbo.IPDHandOverCheckLists");
            DropIndex("dbo.IPDs", new[] { "HandOverCheckListId" });
            DropIndex("dbo.IPDs", new[] { "PrimaryNurseId" });
            DropIndex("dbo.IPDs", new[] { "PrimaryDoctorId" });
            DropIndex("dbo.IPDs", new[] { "ClinicId" });
            DropIndex("dbo.IPDs", new[] { "SpecialtyId" });
            DropIndex("dbo.IPDs", new[] { "SiteId" });
            DropIndex("dbo.IPDs", new[] { "EDStatusId" });
            DropIndex("dbo.IPDs", new[] { "CustomerId" });
            DropIndex("dbo.IPDs", new[] { "RecordCode" });
            DropIndex("dbo.IPDs", new[] { "VisitCode" });
            DropIndex("dbo.IPDHandOverCheckLists", new[] { "ReceivingUnitNurseId" });
            DropIndex("dbo.IPDHandOverCheckLists", new[] { "ReceivingNurseId" });
            DropIndex("dbo.IPDHandOverCheckLists", new[] { "HandOverUnitNurseId" });
            DropIndex("dbo.IPDHandOverCheckLists", new[] { "HandOverNurseId" });
            DropIndex("dbo.IPDHandOverCheckLists", new[] { "ReceivingUnitPhysicianId" });
            DropIndex("dbo.IPDHandOverCheckLists", new[] { "ReceivingPhysicianId" });
            DropIndex("dbo.IPDHandOverCheckLists", new[] { "HandOverUnitPhysicianId" });
            DropIndex("dbo.IPDHandOverCheckLists", new[] { "HandOverPhysicianId" });
            DropIndex("dbo.IPDHandOverCheckListDatas", new[] { "HandOverCheckListId" });
            DropTable("dbo.IPDs");
            DropTable("dbo.IPDHandOverCheckLists");
            DropTable("dbo.IPDHandOverCheckListDatas");
        }
    }
}
