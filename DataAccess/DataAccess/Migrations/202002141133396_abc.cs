namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abc : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Roles", name: "SiteId", newName: "Site_Id");
            RenameColumn(table: "dbo.Users", name: "Role_Id", newName: "CurrentRoleId");
            CreateTable(
                "dbo.OPDs",
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
                        VisitCode = c.String(),
                        HealthInsuranceNumber = c.String(),
                        StartHealthInsuranceDate = c.DateTime(),
                        ExpireHealthInsuranceDate = c.DateTime(),
                        SiteId = c.Guid(),
                        SpecialtyId = c.Guid(),
                        CustomerId = c.Guid(),
                        EDStatusId = c.Guid(),
                        OPDFallRiskScreeningId = c.Guid(),
                        OPDInitialAssessmentForOnGoingId = c.Guid(),
                        OPDInitialAssessmentForShortTermId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .ForeignKey("dbo.EDStatus", t => t.EDStatusId)
                .ForeignKey("dbo.OPDFallRiskScreenings", t => t.OPDFallRiskScreeningId)
                .ForeignKey("dbo.OPDInitialAssessmentForOnGoings", t => t.OPDInitialAssessmentForOnGoingId)
                .ForeignKey("dbo.OPDInitialAssessmentForShortTerms", t => t.OPDInitialAssessmentForShortTermId)
                .ForeignKey("dbo.Sites", t => t.SiteId)
                .ForeignKey("dbo.Specialties", t => t.SpecialtyId)
                .Index(t => t.SiteId)
                .Index(t => t.SpecialtyId)
                .Index(t => t.CustomerId)
                .Index(t => t.EDStatusId)
                .Index(t => t.OPDFallRiskScreeningId)
                .Index(t => t.OPDInitialAssessmentForOnGoingId)
                .Index(t => t.OPDInitialAssessmentForShortTermId);
            
            CreateTable(
                "dbo.OPDFallRiskScreenings",
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
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OPDFallRiskScreeningDatas",
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
                        OPDFallRiskScreeningId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OPDFallRiskScreenings", t => t.OPDFallRiskScreeningId)
                .Index(t => t.OPDFallRiskScreeningId);
            
            CreateTable(
                "dbo.OPDInitialAssessmentForOnGoings",
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
                        RoomID = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rooms", t => t.RoomID)
                .Index(t => t.RoomID);
            
            CreateTable(
                "dbo.OPDInitialAssessmentForOnGoingDatas",
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
                        OPDInitialAssessmentForOnGoingId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OPDInitialAssessmentForOnGoings", t => t.OPDInitialAssessmentForOnGoingId)
                .Index(t => t.OPDInitialAssessmentForOnGoingId);
            
            CreateTable(
                "dbo.VisitTypeGroups",
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
                        ViName = c.String(),
                        EnName = c.String(),
                        Code = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RoleSpecialties",
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
                        RoleId = c.Guid(),
                        SpecialtyId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId)
                .ForeignKey("dbo.Specialties", t => t.SpecialtyId)
                .Index(t => t.RoleId)
                .Index(t => t.SpecialtyId);
            
            CreateTable(
                "dbo.Specialties",
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
                        ViName = c.String(),
                        EnName = c.String(),
                        Code = c.String(),
                        SiteId = c.Guid(),
                        VisitTypeGroupId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sites", t => t.SiteId)
                .ForeignKey("dbo.VisitTypeGroups", t => t.VisitTypeGroupId)
                .Index(t => t.SiteId)
                .Index(t => t.VisitTypeGroupId);
            
            CreateTable(
                "dbo.UserPositions",
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
                        ViName = c.String(),
                        EnName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OPDInitialAssessmentForShortTerms",
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
                        RoomID = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rooms", t => t.RoomID)
                .Index(t => t.RoomID);
            
            CreateTable(
                "dbo.OPDInitialAssessmentForShortTermDatas",
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
                        OPDInitialAssessmentForShortTermId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OPDInitialAssessmentForShortTerms", t => t.OPDInitialAssessmentForShortTermId)
                .Index(t => t.OPDInitialAssessmentForShortTermId);
            
            AddColumn("dbo.EDs", "SpecialtyId", c => c.Guid());
            AddColumn("dbo.EDStatus", "VisitTypeGroupId", c => c.Guid());
            AddColumn("dbo.Orders", "LastDoseDate", c => c.DateTime());
            AddColumn("dbo.Orders", "OPDInitialAssessmentForShortTermId", c => c.Guid());
            AddColumn("dbo.Orders", "OPDInitialAssessmentForOnGoingId", c => c.Guid());
            AddColumn("dbo.Users", "CurrentSpecialtyId", c => c.Guid());
            AddColumn("dbo.Users", "UserPositionId", c => c.Guid());
            AddColumn("dbo.Sites", "ApiCode", c => c.String());
            AddColumn("dbo.Roles", "VisitTypeGroupId", c => c.Guid());
            AddColumn("dbo.Actions", "VisitTypeGroupId", c => c.Guid());
            CreateIndex("dbo.EDs", "SpecialtyId");
            CreateIndex("dbo.EDStatus", "VisitTypeGroupId");
            CreateIndex("dbo.Orders", "OPDInitialAssessmentForShortTermId");
            CreateIndex("dbo.Orders", "OPDInitialAssessmentForOnGoingId");
            CreateIndex("dbo.Users", "CurrentSpecialtyId");
            CreateIndex("dbo.Users", "UserPositionId");
            CreateIndex("dbo.Users", "CurrentRoleId");
            CreateIndex("dbo.Actions", "VisitTypeGroupId");
            AddForeignKey("dbo.Actions", "VisitTypeGroupId", "dbo.VisitTypeGroups", "Id");
            AddForeignKey("dbo.Users", "CurrentSpecialtyId", "dbo.Specialties", "Id");
            AddForeignKey("dbo.Users", "UserPositionId", "dbo.UserPositions", "Id");
            AddForeignKey("dbo.Orders", "OPDInitialAssessmentForShortTermId", "dbo.OPDInitialAssessmentForShortTerms", "Id");
            AddForeignKey("dbo.Orders", "OPDInitialAssessmentForOnGoingId", "dbo.OPDInitialAssessmentForOnGoings", "Id");
            AddForeignKey("dbo.EDStatus", "VisitTypeGroupId", "dbo.VisitTypeGroups", "Id");
            AddForeignKey("dbo.EDs", "SpecialtyId", "dbo.Specialties", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EDs", "SpecialtyId", "dbo.Specialties");
            DropForeignKey("dbo.EDStatus", "VisitTypeGroupId", "dbo.VisitTypeGroups");
            DropForeignKey("dbo.OPDs", "SpecialtyId", "dbo.Specialties");
            DropForeignKey("dbo.OPDs", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.Orders", "OPDInitialAssessmentForOnGoingId", "dbo.OPDInitialAssessmentForOnGoings");
            DropForeignKey("dbo.OPDInitialAssessmentForShortTerms", "RoomID", "dbo.Rooms");
            DropForeignKey("dbo.Orders", "OPDInitialAssessmentForShortTermId", "dbo.OPDInitialAssessmentForShortTerms");
            DropForeignKey("dbo.OPDs", "OPDInitialAssessmentForShortTermId", "dbo.OPDInitialAssessmentForShortTerms");
            DropForeignKey("dbo.OPDInitialAssessmentForShortTermDatas", "OPDInitialAssessmentForShortTermId", "dbo.OPDInitialAssessmentForShortTerms");
            DropForeignKey("dbo.OPDInitialAssessmentForOnGoings", "RoomID", "dbo.Rooms");
            DropForeignKey("dbo.Users", "UserPositionId", "dbo.UserPositions");
            DropForeignKey("dbo.Users", "CurrentSpecialtyId", "dbo.Specialties");
            DropForeignKey("dbo.Specialties", "VisitTypeGroupId", "dbo.VisitTypeGroups");
            DropForeignKey("dbo.Specialties", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.RoleSpecialties", "SpecialtyId", "dbo.Specialties");
            DropForeignKey("dbo.RoleSpecialties", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Actions", "VisitTypeGroupId", "dbo.VisitTypeGroups");
            DropForeignKey("dbo.OPDs", "OPDInitialAssessmentForOnGoingId", "dbo.OPDInitialAssessmentForOnGoings");
            DropForeignKey("dbo.OPDInitialAssessmentForOnGoingDatas", "OPDInitialAssessmentForOnGoingId", "dbo.OPDInitialAssessmentForOnGoings");
            DropForeignKey("dbo.OPDs", "OPDFallRiskScreeningId", "dbo.OPDFallRiskScreenings");
            DropForeignKey("dbo.OPDFallRiskScreeningDatas", "OPDFallRiskScreeningId", "dbo.OPDFallRiskScreenings");
            DropForeignKey("dbo.OPDs", "EDStatusId", "dbo.EDStatus");
            DropForeignKey("dbo.OPDs", "CustomerId", "dbo.Customers");
            DropIndex("dbo.OPDInitialAssessmentForShortTermDatas", new[] { "OPDInitialAssessmentForShortTermId" });
            DropIndex("dbo.OPDInitialAssessmentForShortTerms", new[] { "RoomID" });
            DropIndex("dbo.Specialties", new[] { "VisitTypeGroupId" });
            DropIndex("dbo.Specialties", new[] { "SiteId" });
            DropIndex("dbo.RoleSpecialties", new[] { "SpecialtyId" });
            DropIndex("dbo.RoleSpecialties", new[] { "RoleId" });
            DropIndex("dbo.Actions", new[] { "VisitTypeGroupId" });
            DropIndex("dbo.Users", new[] { "UserPositionId" });
            DropIndex("dbo.Users", new[] { "CurrentSpecialtyId" });
            DropIndex("dbo.Users", new[] { "CurrentRoleId" });
            DropIndex("dbo.Orders", new[] { "OPDInitialAssessmentForOnGoingId" });
            DropIndex("dbo.Orders", new[] { "OPDInitialAssessmentForShortTermId" });
            DropIndex("dbo.OPDInitialAssessmentForOnGoingDatas", new[] { "OPDInitialAssessmentForOnGoingId" });
            DropIndex("dbo.OPDInitialAssessmentForOnGoings", new[] { "RoomID" });
            DropIndex("dbo.OPDFallRiskScreeningDatas", new[] { "OPDFallRiskScreeningId" });
            DropIndex("dbo.OPDs", new[] { "OPDInitialAssessmentForShortTermId" });
            DropIndex("dbo.OPDs", new[] { "OPDInitialAssessmentForOnGoingId" });
            DropIndex("dbo.OPDs", new[] { "OPDFallRiskScreeningId" });
            DropIndex("dbo.OPDs", new[] { "EDStatusId" });
            DropIndex("dbo.OPDs", new[] { "CustomerId" });
            DropIndex("dbo.OPDs", new[] { "SpecialtyId" });
            DropIndex("dbo.OPDs", new[] { "SiteId" });
            DropIndex("dbo.EDStatus", new[] { "VisitTypeGroupId" });
            DropIndex("dbo.EDs", new[] { "SpecialtyId" });
            DropColumn("dbo.Actions", "VisitTypeGroupId");
            DropColumn("dbo.Roles", "VisitTypeGroupId");
            DropColumn("dbo.Sites", "ApiCode");
            DropColumn("dbo.Users", "UserPositionId");
            DropColumn("dbo.Users", "CurrentSpecialtyId");
            DropColumn("dbo.Orders", "OPDInitialAssessmentForOnGoingId");
            DropColumn("dbo.Orders", "OPDInitialAssessmentForShortTermId");
            DropColumn("dbo.Orders", "LastDoseDate");
            DropColumn("dbo.EDStatus", "VisitTypeGroupId");
            DropColumn("dbo.EDs", "SpecialtyId");
            DropTable("dbo.OPDInitialAssessmentForShortTermDatas");
            DropTable("dbo.OPDInitialAssessmentForShortTerms");
            DropTable("dbo.UserPositions");
            DropTable("dbo.Specialties");
            DropTable("dbo.RoleSpecialties");
            DropTable("dbo.VisitTypeGroups");
            DropTable("dbo.OPDInitialAssessmentForOnGoingDatas");
            DropTable("dbo.OPDInitialAssessmentForOnGoings");
            DropTable("dbo.OPDFallRiskScreeningDatas");
            DropTable("dbo.OPDFallRiskScreenings");
            DropTable("dbo.OPDs");
            RenameColumn(table: "dbo.Users", name: "CurrentRoleId", newName: "Role_Id");
            RenameColumn(table: "dbo.Roles", name: "Site_Id", newName: "SiteId");
        }
    }
}
