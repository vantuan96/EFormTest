namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addrequestsupplyconfirmation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EIOBloodProductDatas",
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
                        Name = c.String(),
                        Code = c.String(),
                        Note = c.String(),
                        Quanlity = c.Int(nullable: false),
                        Capacity = c.Int(nullable: false),
                        IsConfirm = c.Boolean(nullable: false),
                        BloodTypeABO = c.String(),
                        BloodTypeRH = c.String(),
                        Time = c.DateTime(),
                        FormId = c.Guid(),
                        GroupId = c.Guid(),
                        FormName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EIOBloodRequestSupplyAndConfirmations",
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
                        Number = c.Int(nullable: false),
                        IsFrequently = c.Boolean(nullable: false),
                        Diagnosis = c.String(),
                        BloodTypeABO = c.String(),
                        BloodTypeRH = c.String(),
                        TransfusionTime = c.Int(nullable: false),
                        SpecialtyId = c.Guid(),
                        HeadOfDeptId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.HeadOfDeptId)
                .ForeignKey("dbo.Specialties", t => t.SpecialtyId)
                .Index(t => t.SpecialtyId)
                .Index(t => t.HeadOfDeptId);
            
            CreateTable(
                "dbo.EIOBloodSupplyDatas",
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
                        Name = c.String(),
                        Quanlity = c.Int(nullable: false),
                        NurseUser = c.String(),
                        NurseTime = c.DateTime(),
                        CuratorUser = c.String(),
                        CuratorTime = c.DateTime(),
                        ProviderUser = c.String(),
                        ProviderTime = c.DateTime(),
                        EIOBloodRequestSupplyAndConfirmationId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EIOBloodRequestSupplyAndConfirmations", t => t.EIOBloodRequestSupplyAndConfirmationId)
                .Index(t => t.EIOBloodRequestSupplyAndConfirmationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EIOBloodSupplyDatas", "EIOBloodRequestSupplyAndConfirmationId", "dbo.EIOBloodRequestSupplyAndConfirmations");
            DropForeignKey("dbo.EIOBloodRequestSupplyAndConfirmations", "SpecialtyId", "dbo.Specialties");
            DropForeignKey("dbo.EIOBloodRequestSupplyAndConfirmations", "HeadOfDeptId", "dbo.Users");
            DropIndex("dbo.EIOBloodSupplyDatas", new[] { "EIOBloodRequestSupplyAndConfirmationId" });
            DropIndex("dbo.EIOBloodRequestSupplyAndConfirmations", new[] { "HeadOfDeptId" });
            DropIndex("dbo.EIOBloodRequestSupplyAndConfirmations", new[] { "SpecialtyId" });
            DropTable("dbo.EIOBloodSupplyDatas");
            DropTable("dbo.EIOBloodRequestSupplyAndConfirmations");
            DropTable("dbo.EIOBloodProductDatas");
        }
    }
}
