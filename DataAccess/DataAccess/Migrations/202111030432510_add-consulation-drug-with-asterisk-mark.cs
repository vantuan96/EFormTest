namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addconsulationdrugwithasteriskmark : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IPDConsultationDrugWithAnAsteriskMarks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RoomNumber = c.String(),
                        AddmisionDate = c.DateTime(),
                        ConsultationDate = c.DateTime(),
                        Diagnosis = c.String(),
                        AntibioticsTreatmentBefore = c.String(),
                        DiagnosisAfterConsultation = c.String(),
                        HospitalDirectorOrHeadDepartmentTime = c.DateTime(),
                        HospitalDirectorOrHeadDepartmentId = c.Guid(),
                        DoctorTime = c.DateTime(),
                        DoctorId = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.DoctorId)
                .ForeignKey("dbo.Users", t => t.HospitalDirectorOrHeadDepartmentId)
                .Index(t => t.HospitalDirectorOrHeadDepartmentId)
                .Index(t => t.DoctorId);
            
            AddColumn("dbo.IPDs", "IPDConsultationDrugWithAnAsteriskMarkId", c => c.Guid());
            CreateIndex("dbo.IPDs", "IPDConsultationDrugWithAnAsteriskMarkId");
            AddForeignKey("dbo.IPDs", "IPDConsultationDrugWithAnAsteriskMarkId", "dbo.IPDConsultationDrugWithAnAsteriskMarks", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IPDs", "IPDConsultationDrugWithAnAsteriskMarkId", "dbo.IPDConsultationDrugWithAnAsteriskMarks");
            DropForeignKey("dbo.IPDConsultationDrugWithAnAsteriskMarks", "HospitalDirectorOrHeadDepartmentId", "dbo.Users");
            DropForeignKey("dbo.IPDConsultationDrugWithAnAsteriskMarks", "DoctorId", "dbo.Users");
            DropIndex("dbo.IPDs", new[] { "IPDConsultationDrugWithAnAsteriskMarkId" });
            DropIndex("dbo.IPDConsultationDrugWithAnAsteriskMarks", new[] { "DoctorId" });
            DropIndex("dbo.IPDConsultationDrugWithAnAsteriskMarks", new[] { "HospitalDirectorOrHeadDepartmentId" });
            DropColumn("dbo.IPDs", "IPDConsultationDrugWithAnAsteriskMarkId");
            DropTable("dbo.IPDConsultationDrugWithAnAsteriskMarks");
        }
    }
}
