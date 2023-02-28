namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_surgiacl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EIOJointConsultationGroupMinutes",
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
                        VisitTypeGroupCode = c.String(),
                        SpecialtyId = c.Guid(),
                        SecretaryConfirm = c.Boolean(nullable: false),
                        SecretaryId = c.Guid(),
                        ChairmanConfirm = c.Boolean(nullable: false),
                        ChairmanId = c.Guid(),
                        MemberConfirm = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.ChairmanId)
                .ForeignKey("dbo.Users", t => t.SecretaryId)
                .ForeignKey("dbo.Specialties", t => t.SpecialtyId)
                .Index(t => t.SpecialtyId)
                .Index(t => t.SecretaryId)
                .Index(t => t.ChairmanId);
            
            CreateTable(
                "dbo.EIOJointConsultationGroupMinutesDatas",
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
                        EnValue = c.String(),
                        EIOJointConsultationGroupMinutesId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EIOJointConsultationGroupMinutes", t => t.EIOJointConsultationGroupMinutesId)
                .Index(t => t.EIOJointConsultationGroupMinutesId);
            
            CreateTable(
                "dbo.EIOJointConsultationGroupMinutesMembers",
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
                        EIOJointConsultationGroupMinutesId = c.Guid(),
                        IsConfirm = c.Boolean(nullable: false),
                        MemberId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EIOJointConsultationGroupMinutes", t => t.EIOJointConsultationGroupMinutesId)
                .ForeignKey("dbo.Users", t => t.MemberId)
                .Index(t => t.EIOJointConsultationGroupMinutesId)
                .Index(t => t.MemberId);
            
            CreateTable(
                "dbo.EIOSurgicalProcedureSafetyChecklists",
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
                        VisitTypeGroupCode = c.String(),
                        EIOSurgicalProcedureSafetyChecklistSignInId = c.Guid(),
                        EIOSurgicalProcedureSafetyChecklistTimeOutId = c.Guid(),
                        EIOSurgicalProcedureSafetyChecklistSignOutId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EIOSurgicalProcedureSafetyChecklistSignIns", t => t.EIOSurgicalProcedureSafetyChecklistSignInId)
                .ForeignKey("dbo.EIOSurgicalProcedureSafetyChecklistSignOuts", t => t.EIOSurgicalProcedureSafetyChecklistSignOutId)
                .ForeignKey("dbo.EIOSurgicalProcedureSafetyChecklistTimeOuts", t => t.EIOSurgicalProcedureSafetyChecklistTimeOutId)
                .Index(t => t.EIOSurgicalProcedureSafetyChecklistSignInId)
                .Index(t => t.EIOSurgicalProcedureSafetyChecklistTimeOutId)
                .Index(t => t.EIOSurgicalProcedureSafetyChecklistSignOutId);
            
            CreateTable(
                "dbo.EIOSurgicalProcedureSafetyChecklistSignIns",
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
                "dbo.EIOSurgicalProcedureSafetyChecklistSignOuts",
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
                "dbo.EIOSurgicalProcedureSafetyChecklistTimeOuts",
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
                "dbo.EIOSurgicalProcedureSafetyChecklistDatas",
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
                        EIOSurgicalProcedureSafetyChecklistId = c.Guid(),
                        EIOSurgicalProcedureSafetyChecklistCode = c.String(),
                        Code = c.String(),
                        Value = c.String(),
                        EnValue = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EIOSurgicalProcedureSafetyChecklists", "EIOSurgicalProcedureSafetyChecklistTimeOutId", "dbo.EIOSurgicalProcedureSafetyChecklistTimeOuts");
            DropForeignKey("dbo.EIOSurgicalProcedureSafetyChecklists", "EIOSurgicalProcedureSafetyChecklistSignOutId", "dbo.EIOSurgicalProcedureSafetyChecklistSignOuts");
            DropForeignKey("dbo.EIOSurgicalProcedureSafetyChecklists", "EIOSurgicalProcedureSafetyChecklistSignInId", "dbo.EIOSurgicalProcedureSafetyChecklistSignIns");
            DropForeignKey("dbo.EIOJointConsultationGroupMinutes", "SpecialtyId", "dbo.Specialties");
            DropForeignKey("dbo.EIOJointConsultationGroupMinutes", "SecretaryId", "dbo.Users");
            DropForeignKey("dbo.EIOJointConsultationGroupMinutesMembers", "MemberId", "dbo.Users");
            DropForeignKey("dbo.EIOJointConsultationGroupMinutesMembers", "EIOJointConsultationGroupMinutesId", "dbo.EIOJointConsultationGroupMinutes");
            DropForeignKey("dbo.EIOJointConsultationGroupMinutesDatas", "EIOJointConsultationGroupMinutesId", "dbo.EIOJointConsultationGroupMinutes");
            DropForeignKey("dbo.EIOJointConsultationGroupMinutes", "ChairmanId", "dbo.Users");
            DropIndex("dbo.EIOSurgicalProcedureSafetyChecklists", new[] { "EIOSurgicalProcedureSafetyChecklistSignOutId" });
            DropIndex("dbo.EIOSurgicalProcedureSafetyChecklists", new[] { "EIOSurgicalProcedureSafetyChecklistTimeOutId" });
            DropIndex("dbo.EIOSurgicalProcedureSafetyChecklists", new[] { "EIOSurgicalProcedureSafetyChecklistSignInId" });
            DropIndex("dbo.EIOJointConsultationGroupMinutesMembers", new[] { "MemberId" });
            DropIndex("dbo.EIOJointConsultationGroupMinutesMembers", new[] { "EIOJointConsultationGroupMinutesId" });
            DropIndex("dbo.EIOJointConsultationGroupMinutesDatas", new[] { "EIOJointConsultationGroupMinutesId" });
            DropIndex("dbo.EIOJointConsultationGroupMinutes", new[] { "ChairmanId" });
            DropIndex("dbo.EIOJointConsultationGroupMinutes", new[] { "SecretaryId" });
            DropIndex("dbo.EIOJointConsultationGroupMinutes", new[] { "SpecialtyId" });
            DropTable("dbo.EIOSurgicalProcedureSafetyChecklistDatas");
            DropTable("dbo.EIOSurgicalProcedureSafetyChecklistTimeOuts");
            DropTable("dbo.EIOSurgicalProcedureSafetyChecklistSignOuts");
            DropTable("dbo.EIOSurgicalProcedureSafetyChecklistSignIns");
            DropTable("dbo.EIOSurgicalProcedureSafetyChecklists");
            DropTable("dbo.EIOJointConsultationGroupMinutesMembers");
            DropTable("dbo.EIOJointConsultationGroupMinutesDatas");
            DropTable("dbo.EIOJointConsultationGroupMinutes");
        }
    }
}
