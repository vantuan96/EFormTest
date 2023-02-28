namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatesprint23 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OPDNCCNBROV1",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VisitId = c.Guid(),
                        DoctorConfirmId = c.Guid(),
                        DoctorConfirmAt = c.DateTime(),
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
                "dbo.OPDRiskAssessmentForCancers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VisitId = c.Guid(),
                        DoctorConfirmId = c.Guid(),
                        DoctorConfirmAt = c.DateTime(),
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
                "dbo.StillBirths",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VisitId = c.Guid(),
                        VisitTypeGroupCode = c.String(),
                        HospitalLeadershipConfirmId = c.Guid(),
                        HospitalLeadershipConfirmAt = c.DateTime(),
                        HeadOfDepartmentOrLeaderOfOnDutyTeamConfirmId = c.Guid(),
                        HeadOfDepartmentOrLeaderOfOnDutyTeamConfirmAt = c.DateTime(),
                        PatientOrPatientIsFamilyConfirmId = c.Guid(),
                        PatientOrPatientIsFamilyConfirmAt = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.StillBirths");
            DropTable("dbo.OPDRiskAssessmentForCancers");
            DropTable("dbo.OPDNCCNBROV1");
        }
    }
}
