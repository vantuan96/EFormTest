namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createclinic : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserClinics",
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
                        ClinicId = c.Guid(),
                        UserId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clinics", t => t.ClinicId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.ClinicId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Clinics",
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
                        SpecialtyId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Specialties", t => t.SpecialtyId)
                .Index(t => t.SpecialtyId);
            
            AddColumn("dbo.OPDOutpatientExaminationNotes", "BlockTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserClinics", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserClinics", "ClinicId", "dbo.Clinics");
            DropForeignKey("dbo.Clinics", "SpecialtyId", "dbo.Specialties");
            DropIndex("dbo.Clinics", new[] { "SpecialtyId" });
            DropIndex("dbo.UserClinics", new[] { "UserId" });
            DropIndex("dbo.UserClinics", new[] { "ClinicId" });
            DropColumn("dbo.OPDOutpatientExaminationNotes", "BlockTime");
            DropTable("dbo.Clinics");
            DropTable("dbo.UserClinics");
        }
    }
}
