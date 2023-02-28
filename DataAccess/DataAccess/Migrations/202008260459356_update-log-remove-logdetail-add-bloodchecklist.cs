namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatelogremovelogdetailaddbloodchecklist : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.LogDetails", new[] { "RequestId" });
            DropIndex("dbo.Logs", new[] { "RequestId" });
            CreateTable(
                "dbo.EIOBloodTransfusionChecklistDatas",
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
                        EIOBloodTransfusionChecklistId = c.Guid(),
                        Time = c.DateTime(),
                        TransfusionSpeed = c.String(),
                        ColorOfSkin = c.String(),
                        BreathsPerMinute = c.String(),
                        PulsePerMinute = c.String(),
                        Temp = c.Single(nullable: false),
                        Other = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EIOBloodTransfusionChecklists", t => t.EIOBloodTransfusionChecklistId)
                .Index(t => t.EIOBloodTransfusionChecklistId);
            
            CreateTable(
                "dbo.EIOBloodTransfusionChecklists",
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
                        Diagnosis = c.String(),
                        SpecialtyId = c.Guid(),
                        BedNo = c.String(),
                        TypeOfBloodProducts = c.String(),
                        Quanlity = c.Int(nullable: false),
                        Code = c.String(),
                        DateOfBloodCollection = c.DateTime(),
                        Expiry = c.DateTime(),
                        PatientBloodTypeABO = c.String(),
                        PatientBloodTypeRH = c.String(),
                        DonorBloodTypeABO = c.String(),
                        DonorBloodTypeRH = c.String(),
                        OtherTests = c.String(),
                        MajorCrossMatchSalt = c.String(),
                        MajorCrossMatchAntiGlobulin = c.String(),
                        MinorCrossMatchSalt = c.String(),
                        MinorCrossMatchAntiGlobulin = c.String(),
                        HeadOfLabConfirmTime = c.DateTime(),
                        HeadOfLabId = c.Guid(),
                        FirstTechnicianConfirmTime = c.DateTime(),
                        FirstTechnicianId = c.Guid(),
                        SecondTechnicianConfirmTime = c.DateTime(),
                        SecondTechnicianId = c.Guid(),
                        NumberOfBloodTransfusion = c.String(),
                        Crossmatch = c.String(),
                        StartTransfusionAt = c.DateTime(),
                        StopTransfusionAt = c.DateTime(),
                        ActualAmountOfBloodTransmitted = c.Int(nullable: false),
                        Remark = c.String(),
                        PhysicianConfirmTime = c.DateTime(),
                        PhysicianId = c.Guid(),
                        NurseConfirmTime = c.DateTime(),
                        NurseId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.FirstTechnicianId)
                .ForeignKey("dbo.Users", t => t.HeadOfLabId)
                .ForeignKey("dbo.Users", t => t.NurseId)
                .ForeignKey("dbo.Users", t => t.PhysicianId)
                .ForeignKey("dbo.Users", t => t.SecondTechnicianId)
                .ForeignKey("dbo.Specialties", t => t.SpecialtyId)
                .Index(t => t.SpecialtyId)
                .Index(t => t.HeadOfLabId)
                .Index(t => t.FirstTechnicianId)
                .Index(t => t.SecondTechnicianId)
                .Index(t => t.PhysicianId)
                .Index(t => t.NurseId);
            
            DropColumn("dbo.Logs", "UpdatedAt");
            DropColumn("dbo.Logs", "UpdatedBy");
            DropColumn("dbo.Logs", "IsDeleted");
            DropColumn("dbo.Logs", "DeletedAt");
            DropColumn("dbo.Logs", "DeletedBy");
            DropColumn("dbo.Logs", "RequestId");
            DropTable("dbo.LogDetails");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.LogDetails",
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
                        ModelId = c.Guid(),
                        ModelName = c.String(),
                        Action = c.String(),
                        Before = c.String(),
                        After = c.String(),
                        RequestId = c.String(maxLength: 80, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Logs", "RequestId", c => c.String(maxLength: 80, unicode: false));
            AddColumn("dbo.Logs", "DeletedBy", c => c.String());
            AddColumn("dbo.Logs", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.Logs", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Logs", "UpdatedBy", c => c.String());
            AddColumn("dbo.Logs", "UpdatedAt", c => c.DateTime());
            DropForeignKey("dbo.EIOBloodTransfusionChecklistDatas", "EIOBloodTransfusionChecklistId", "dbo.EIOBloodTransfusionChecklists");
            DropForeignKey("dbo.EIOBloodTransfusionChecklists", "SpecialtyId", "dbo.Specialties");
            DropForeignKey("dbo.EIOBloodTransfusionChecklists", "SecondTechnicianId", "dbo.Users");
            DropForeignKey("dbo.EIOBloodTransfusionChecklists", "PhysicianId", "dbo.Users");
            DropForeignKey("dbo.EIOBloodTransfusionChecklists", "NurseId", "dbo.Users");
            DropForeignKey("dbo.EIOBloodTransfusionChecklists", "HeadOfLabId", "dbo.Users");
            DropForeignKey("dbo.EIOBloodTransfusionChecklists", "FirstTechnicianId", "dbo.Users");
            DropIndex("dbo.EIOBloodTransfusionChecklists", new[] { "NurseId" });
            DropIndex("dbo.EIOBloodTransfusionChecklists", new[] { "PhysicianId" });
            DropIndex("dbo.EIOBloodTransfusionChecklists", new[] { "SecondTechnicianId" });
            DropIndex("dbo.EIOBloodTransfusionChecklists", new[] { "FirstTechnicianId" });
            DropIndex("dbo.EIOBloodTransfusionChecklists", new[] { "HeadOfLabId" });
            DropIndex("dbo.EIOBloodTransfusionChecklists", new[] { "SpecialtyId" });
            DropIndex("dbo.EIOBloodTransfusionChecklistDatas", new[] { "EIOBloodTransfusionChecklistId" });
            DropTable("dbo.EIOBloodTransfusionChecklists");
            DropTable("dbo.EIOBloodTransfusionChecklistDatas");
            CreateIndex("dbo.Logs", "RequestId");
            CreateIndex("dbo.LogDetails", "RequestId");
        }
    }
}
