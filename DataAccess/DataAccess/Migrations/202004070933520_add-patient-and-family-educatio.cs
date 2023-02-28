namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addpatientandfamilyeducatio : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ICD10", "ClinicId", "dbo.Clinics");
            DropIndex("dbo.ICD10", new[] { "ClinicId" });
            CreateTable(
                "dbo.ICDSpecialties",
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
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PatientAndFamilyEducationContentDatas",
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
                        PatientAndFamilyEducationContentId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PatientAndFamilyEducationContents", t => t.PatientAndFamilyEducationContentId)
                .Index(t => t.PatientAndFamilyEducationContentId);
            
            CreateTable(
                "dbo.PatientAndFamilyEducationContents",
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
                        EducationalNeedCode = c.String(),
                        PatientAndFamilyEducationId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PatientAndFamilyEducations", t => t.PatientAndFamilyEducationId)
                .Index(t => t.PatientAndFamilyEducationId);
            
            CreateTable(
                "dbo.PatientAndFamilyEducations",
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
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ICD10", "ICDSpecialtyId", c => c.Guid());
            CreateIndex("dbo.ICD10", "ICDSpecialtyId");
            AddForeignKey("dbo.ICD10", "ICDSpecialtyId", "dbo.ICDSpecialties", "Id");
            DropColumn("dbo.ICD10", "ClinicId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ICD10", "ClinicId", c => c.Guid());
            DropForeignKey("dbo.PatientAndFamilyEducationContentDatas", "PatientAndFamilyEducationContentId", "dbo.PatientAndFamilyEducationContents");
            DropForeignKey("dbo.PatientAndFamilyEducationContents", "PatientAndFamilyEducationId", "dbo.PatientAndFamilyEducations");
            DropForeignKey("dbo.ICD10", "ICDSpecialtyId", "dbo.ICDSpecialties");
            DropIndex("dbo.PatientAndFamilyEducationContents", new[] { "PatientAndFamilyEducationId" });
            DropIndex("dbo.PatientAndFamilyEducationContentDatas", new[] { "PatientAndFamilyEducationContentId" });
            DropIndex("dbo.ICD10", new[] { "ICDSpecialtyId" });
            DropColumn("dbo.ICD10", "ICDSpecialtyId");
            DropTable("dbo.PatientAndFamilyEducations");
            DropTable("dbo.PatientAndFamilyEducationContents");
            DropTable("dbo.PatientAndFamilyEducationContentDatas");
            DropTable("dbo.ICDSpecialties");
            CreateIndex("dbo.ICD10", "ClinicId");
            AddForeignKey("dbo.ICD10", "ClinicId", "dbo.Clinics", "Id");
        }
    }
}
