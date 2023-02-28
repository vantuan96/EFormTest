namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addassessmentexternaltran : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EIOExternalTransportationAssessmentDatas",
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
                        EIOExternalTransportationAssessmentId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EIOExternalTransportationAssessments", t => t.EIOExternalTransportationAssessmentId)
                .Index(t => t.EIOExternalTransportationAssessmentId);
            
            CreateTable(
                "dbo.EIOExternalTransportationAssessments",
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
                        NurseTime = c.DateTime(),
                        NurseId = c.Guid(),
                        DoctorTime = c.DateTime(),
                        DoctorId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.DoctorId)
                .ForeignKey("dbo.Users", t => t.NurseId)
                .Index(t => t.NurseId)
                .Index(t => t.DoctorId);
            
            CreateTable(
                "dbo.EIOExternalTransportationAssessmentEquipments",
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
                        IsNeeded = c.Boolean(),
                        Note = c.String(),
                        EIOExternalTransportationAssessmentId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EIOExternalTransportationAssessments", t => t.EIOExternalTransportationAssessmentId)
                .Index(t => t.EIOExternalTransportationAssessmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EIOExternalTransportationAssessments", "NurseId", "dbo.Users");
            DropForeignKey("dbo.EIOExternalTransportationAssessmentEquipments", "EIOExternalTransportationAssessmentId", "dbo.EIOExternalTransportationAssessments");
            DropForeignKey("dbo.EIOExternalTransportationAssessmentDatas", "EIOExternalTransportationAssessmentId", "dbo.EIOExternalTransportationAssessments");
            DropForeignKey("dbo.EIOExternalTransportationAssessments", "DoctorId", "dbo.Users");
            DropIndex("dbo.EIOExternalTransportationAssessmentEquipments", new[] { "EIOExternalTransportationAssessmentId" });
            DropIndex("dbo.EIOExternalTransportationAssessments", new[] { "DoctorId" });
            DropIndex("dbo.EIOExternalTransportationAssessments", new[] { "NurseId" });
            DropIndex("dbo.EIOExternalTransportationAssessmentDatas", new[] { "EIOExternalTransportationAssessmentId" });
            DropTable("dbo.EIOExternalTransportationAssessmentEquipments");
            DropTable("dbo.EIOExternalTransportationAssessments");
            DropTable("dbo.EIOExternalTransportationAssessmentDatas");
        }
    }
}
