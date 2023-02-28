namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addpatientownmedicationchart : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EIOPatientOwnMedicationsChartDatas",
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
                        DrugName = c.String(),
                        Unit = c.String(),
                        Quantity = c.String(),
                        LotNo = c.String(),
                        ExpiryDate = c.DateTime(),
                        Note = c.String(),
                        EIOPatientOwnMedicationsChartId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EIOPatientOwnMedicationsCharts", t => t.EIOPatientOwnMedicationsChartId)
                .Index(t => t.EIOPatientOwnMedicationsChartId);
            
            CreateTable(
                "dbo.EIOPatientOwnMedicationsCharts",
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
                        FirstTotal = c.String(),
                        DoctorOpinion = c.String(),
                        ClinicalPharmacistReview = c.String(),
                        SecondTotal = c.String(),
                        Upload = c.String(),
                        HeadOfPharmacyTime = c.DateTime(),
                        HeadOfPharmacyId = c.Guid(),
                        HeadOfDepartmentTime = c.DateTime(),
                        HeadOfDepartmentId = c.Guid(),
                        DoctorTime = c.DateTime(),
                        DoctorId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.DoctorId)
                .ForeignKey("dbo.Users", t => t.HeadOfDepartmentId)
                .ForeignKey("dbo.Users", t => t.HeadOfPharmacyId)
                .Index(t => t.HeadOfPharmacyId)
                .Index(t => t.HeadOfDepartmentId)
                .Index(t => t.DoctorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EIOPatientOwnMedicationsCharts", "HeadOfPharmacyId", "dbo.Users");
            DropForeignKey("dbo.EIOPatientOwnMedicationsCharts", "HeadOfDepartmentId", "dbo.Users");
            DropForeignKey("dbo.EIOPatientOwnMedicationsChartDatas", "EIOPatientOwnMedicationsChartId", "dbo.EIOPatientOwnMedicationsCharts");
            DropForeignKey("dbo.EIOPatientOwnMedicationsCharts", "DoctorId", "dbo.Users");
            DropIndex("dbo.EIOPatientOwnMedicationsCharts", new[] { "DoctorId" });
            DropIndex("dbo.EIOPatientOwnMedicationsCharts", new[] { "HeadOfDepartmentId" });
            DropIndex("dbo.EIOPatientOwnMedicationsCharts", new[] { "HeadOfPharmacyId" });
            DropIndex("dbo.EIOPatientOwnMedicationsChartDatas", new[] { "EIOPatientOwnMedicationsChartId" });
            DropTable("dbo.EIOPatientOwnMedicationsCharts");
            DropTable("dbo.EIOPatientOwnMedicationsChartDatas");
        }
    }
}
