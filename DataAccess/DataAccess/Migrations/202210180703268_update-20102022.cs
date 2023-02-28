namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update20102022 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.IPDSetupMedicalRecords");
            CreateTable(
                "dbo.EIOConstraintNewbornAndPregnantWomen",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VisitId = c.Guid(nullable: false),
                        NewbornCustomerId = c.Guid(),
                        VisitTypeCode = c.String(),
                        PregnantWomanCustomerId = c.Guid(nullable: false),
                        FormCode = c.String(),
                        Room = c.String(),
                        Bed = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.NewbornCustomerId)
                .Index(t => t.NewbornCustomerId);
            
            CreateTable(
                "dbo.OPDPreAnesthesiaHandOverCheckLists",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VisitId = c.Guid(nullable: false),
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
                        Code = c.String(),
                        IsUseHandOverCheckList = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
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
            
            AddColumn("dbo.Specialties", "IsAnesthesia", c => c.Boolean(nullable: false));
            AddColumn("dbo.EIOCareNotes", "FormId", c => c.Guid());
            AddColumn("dbo.EIOPhysicianNotes", "FormId", c => c.Guid());
            AddColumn("dbo.IPDInitialAssessmentForNewborns", "EIOConstraintNewbornAndPregnantWomanId", c => c.Guid());
            AddColumn("dbo.IPDInitialAssessmentForNewborns", "Version", c => c.Int(nullable: false));
            AddColumn("dbo.IPDSetupMedicalRecords", "Id", c => c.Guid(nullable: false, defaultValueSql: "NewId()"));
            AddColumn("dbo.IPDSetupMedicalRecords", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.IPDSetupMedicalRecords", "DeletedBy", c => c.String());
            AddColumn("dbo.IPDSetupMedicalRecords", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.IPDSetupMedicalRecords", "CreatedBy", c => c.String());
            AddColumn("dbo.IPDSetupMedicalRecords", "CreatedAt", c => c.DateTime());
            AddColumn("dbo.IPDSetupMedicalRecords", "UpdatedBy", c => c.String());
            AddColumn("dbo.IPDSetupMedicalRecords", "UpdatedAt", c => c.DateTime());
            AddColumn("dbo.OPDs", "IsAnesthesia", c => c.Boolean(nullable: false));
            AddColumn("dbo.Translations", "SpecialtyId", c => c.Guid());
            AddColumn("dbo.UploadImages", "VisitId", c => c.Guid(nullable: false));
            AlterColumn("dbo.IPDSetupMedicalRecords", "Formcode", c => c.String(maxLength: 50));
            AddPrimaryKey("dbo.IPDSetupMedicalRecords", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OPDPreAnesthesiaHandOverCheckLists", "ReceivingUnitPhysicianId", "dbo.Specialties");
            DropForeignKey("dbo.OPDPreAnesthesiaHandOverCheckLists", "ReceivingUnitNurseId", "dbo.Specialties");
            DropForeignKey("dbo.OPDPreAnesthesiaHandOverCheckLists", "ReceivingPhysicianId", "dbo.Users");
            DropForeignKey("dbo.OPDPreAnesthesiaHandOverCheckLists", "ReceivingNurseId", "dbo.Users");
            DropForeignKey("dbo.OPDPreAnesthesiaHandOverCheckLists", "HandOverUnitPhysicianId", "dbo.Specialties");
            DropForeignKey("dbo.OPDPreAnesthesiaHandOverCheckLists", "HandOverUnitNurseId", "dbo.Specialties");
            DropForeignKey("dbo.OPDPreAnesthesiaHandOverCheckLists", "HandOverPhysicianId", "dbo.Users");
            DropForeignKey("dbo.OPDPreAnesthesiaHandOverCheckLists", "HandOverNurseId", "dbo.Users");
            DropForeignKey("dbo.EIOConstraintNewbornAndPregnantWomen", "NewbornCustomerId", "dbo.Customers");
            DropIndex("dbo.OPDPreAnesthesiaHandOverCheckLists", new[] { "ReceivingUnitNurseId" });
            DropIndex("dbo.OPDPreAnesthesiaHandOverCheckLists", new[] { "ReceivingNurseId" });
            DropIndex("dbo.OPDPreAnesthesiaHandOverCheckLists", new[] { "HandOverUnitNurseId" });
            DropIndex("dbo.OPDPreAnesthesiaHandOverCheckLists", new[] { "HandOverNurseId" });
            DropIndex("dbo.OPDPreAnesthesiaHandOverCheckLists", new[] { "ReceivingUnitPhysicianId" });
            DropIndex("dbo.OPDPreAnesthesiaHandOverCheckLists", new[] { "ReceivingPhysicianId" });
            DropIndex("dbo.OPDPreAnesthesiaHandOverCheckLists", new[] { "HandOverUnitPhysicianId" });
            DropIndex("dbo.OPDPreAnesthesiaHandOverCheckLists", new[] { "HandOverPhysicianId" });
            DropIndex("dbo.EIOConstraintNewbornAndPregnantWomen", new[] { "NewbornCustomerId" });
            DropPrimaryKey("dbo.IPDSetupMedicalRecords");
            AlterColumn("dbo.IPDSetupMedicalRecords", "Formcode", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.UploadImages", "VisitId");
            DropColumn("dbo.Translations", "SpecialtyId");
            DropColumn("dbo.OPDs", "IsAnesthesia");
            DropColumn("dbo.IPDSetupMedicalRecords", "UpdatedAt");
            DropColumn("dbo.IPDSetupMedicalRecords", "UpdatedBy");
            DropColumn("dbo.IPDSetupMedicalRecords", "CreatedAt");
            DropColumn("dbo.IPDSetupMedicalRecords", "CreatedBy");
            DropColumn("dbo.IPDSetupMedicalRecords", "DeletedAt");
            DropColumn("dbo.IPDSetupMedicalRecords", "DeletedBy");
            DropColumn("dbo.IPDSetupMedicalRecords", "IsDeleted");
            DropColumn("dbo.IPDSetupMedicalRecords", "Id");
            DropColumn("dbo.IPDInitialAssessmentForNewborns", "Version");
            DropColumn("dbo.IPDInitialAssessmentForNewborns", "EIOConstraintNewbornAndPregnantWomanId");
            DropColumn("dbo.EIOPhysicianNotes", "FormId");
            DropColumn("dbo.EIOCareNotes", "FormId");
            DropColumn("dbo.Specialties", "IsAnesthesia");
            DropTable("dbo.OPDPreAnesthesiaHandOverCheckLists");
            DropTable("dbo.EIOConstraintNewbornAndPregnantWomen");
            AddPrimaryKey("dbo.IPDSetupMedicalRecords", new[] { "SpecialityId", "Formcode" });
        }
    }
}
