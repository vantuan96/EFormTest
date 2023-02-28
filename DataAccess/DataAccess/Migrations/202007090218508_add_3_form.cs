namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_3_form : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EDConsultationDrugWithAnAsteriskMarks",
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
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.DoctorId)
                .ForeignKey("dbo.Users", t => t.HospitalDirectorOrHeadDepartmentId)
                .Index(t => t.HospitalDirectorOrHeadDepartmentId)
                .Index(t => t.DoctorId);
            
            CreateTable(
                "dbo.EDJointConsultationForApprovalOfSurgeries",
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
                        TimeOfAdmission = c.DateTime(),
                        TimeOfJointConsultation = c.DateTime(),
                        CMOId = c.Guid(),
                        HeadOfDeptId = c.Guid(),
                        AnesthetistId = c.Guid(),
                        SurgeonId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AnesthetistId)
                .ForeignKey("dbo.Users", t => t.CMOId)
                .ForeignKey("dbo.Users", t => t.HeadOfDeptId)
                .ForeignKey("dbo.Users", t => t.SurgeonId)
                .Index(t => t.CMOId)
                .Index(t => t.HeadOfDeptId)
                .Index(t => t.AnesthetistId)
                .Index(t => t.SurgeonId);
            
            CreateTable(
                "dbo.EDJointConsultationForApprovalOfSurgeryDatas",
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
                        EDJointConsultationForApprovalOfSurgeryId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EDJointConsultationForApprovalOfSurgeries", t => t.EDJointConsultationForApprovalOfSurgeryId)
                .Index(t => t.EDJointConsultationForApprovalOfSurgeryId);
            
            CreateTable(
                "dbo.EDSkinTestResults",
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
                        Conclusion = c.String(),
                        ConfirmDate = c.DateTime(),
                        ConfirmDoctorId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.ConfirmDoctorId)
                .Index(t => t.ConfirmDoctorId);
            
            CreateTable(
                "dbo.EDSkinTestResultDatas",
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
                        EDSkinTestResultId = c.Guid(),
                        Drug = c.String(),
                        SkinDilutionConcentration = c.String(),
                        SkinResult = c.String(),
                        SkinPositive = c.String(),
                        SkinNegative = c.String(),
                        EndodermDilutionConcentration = c.String(),
                        EndodermResult = c.String(),
                        EndodermNegative = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EDSkinTestResults", t => t.EDSkinTestResultId)
                .Index(t => t.EDSkinTestResultId);
            
            AddColumn("dbo.Orders", "Reason", c => c.String());
            AddColumn("dbo.Orders", "Concentration", c => c.String());
            AddColumn("dbo.EDs", "EDSkinTestResultId", c => c.Guid());
            AddColumn("dbo.EDs", "EDConsultationDrugWithAnAsteriskMarkId", c => c.Guid());
            AddColumn("dbo.EDs", "EDJointConsultationForApprovalOfSurgeryId", c => c.Guid());
            CreateIndex("dbo.EDs", "EDSkinTestResultId");
            CreateIndex("dbo.EDs", "EDConsultationDrugWithAnAsteriskMarkId");
            CreateIndex("dbo.EDs", "EDJointConsultationForApprovalOfSurgeryId");
            AddForeignKey("dbo.EDs", "EDConsultationDrugWithAnAsteriskMarkId", "dbo.EDConsultationDrugWithAnAsteriskMarks", "Id");
            AddForeignKey("dbo.EDs", "EDJointConsultationForApprovalOfSurgeryId", "dbo.EDJointConsultationForApprovalOfSurgeries", "Id");
            AddForeignKey("dbo.EDs", "EDSkinTestResultId", "dbo.EDSkinTestResults", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EDs", "EDSkinTestResultId", "dbo.EDSkinTestResults");
            DropForeignKey("dbo.EDSkinTestResultDatas", "EDSkinTestResultId", "dbo.EDSkinTestResults");
            DropForeignKey("dbo.EDSkinTestResults", "ConfirmDoctorId", "dbo.Users");
            DropForeignKey("dbo.EDs", "EDJointConsultationForApprovalOfSurgeryId", "dbo.EDJointConsultationForApprovalOfSurgeries");
            DropForeignKey("dbo.EDJointConsultationForApprovalOfSurgeries", "SurgeonId", "dbo.Users");
            DropForeignKey("dbo.EDJointConsultationForApprovalOfSurgeries", "HeadOfDeptId", "dbo.Users");
            DropForeignKey("dbo.EDJointConsultationForApprovalOfSurgeryDatas", "EDJointConsultationForApprovalOfSurgeryId", "dbo.EDJointConsultationForApprovalOfSurgeries");
            DropForeignKey("dbo.EDJointConsultationForApprovalOfSurgeries", "CMOId", "dbo.Users");
            DropForeignKey("dbo.EDJointConsultationForApprovalOfSurgeries", "AnesthetistId", "dbo.Users");
            DropForeignKey("dbo.EDs", "EDConsultationDrugWithAnAsteriskMarkId", "dbo.EDConsultationDrugWithAnAsteriskMarks");
            DropForeignKey("dbo.EDConsultationDrugWithAnAsteriskMarks", "HospitalDirectorOrHeadDepartmentId", "dbo.Users");
            DropForeignKey("dbo.EDConsultationDrugWithAnAsteriskMarks", "DoctorId", "dbo.Users");
            DropIndex("dbo.EDSkinTestResultDatas", new[] { "EDSkinTestResultId" });
            DropIndex("dbo.EDSkinTestResults", new[] { "ConfirmDoctorId" });
            DropIndex("dbo.EDJointConsultationForApprovalOfSurgeryDatas", new[] { "EDJointConsultationForApprovalOfSurgeryId" });
            DropIndex("dbo.EDJointConsultationForApprovalOfSurgeries", new[] { "SurgeonId" });
            DropIndex("dbo.EDJointConsultationForApprovalOfSurgeries", new[] { "AnesthetistId" });
            DropIndex("dbo.EDJointConsultationForApprovalOfSurgeries", new[] { "HeadOfDeptId" });
            DropIndex("dbo.EDJointConsultationForApprovalOfSurgeries", new[] { "CMOId" });
            DropIndex("dbo.EDConsultationDrugWithAnAsteriskMarks", new[] { "DoctorId" });
            DropIndex("dbo.EDConsultationDrugWithAnAsteriskMarks", new[] { "HospitalDirectorOrHeadDepartmentId" });
            DropIndex("dbo.EDs", new[] { "EDJointConsultationForApprovalOfSurgeryId" });
            DropIndex("dbo.EDs", new[] { "EDConsultationDrugWithAnAsteriskMarkId" });
            DropIndex("dbo.EDs", new[] { "EDSkinTestResultId" });
            DropColumn("dbo.EDs", "EDJointConsultationForApprovalOfSurgeryId");
            DropColumn("dbo.EDs", "EDConsultationDrugWithAnAsteriskMarkId");
            DropColumn("dbo.EDs", "EDSkinTestResultId");
            DropColumn("dbo.Orders", "Concentration");
            DropColumn("dbo.Orders", "Reason");
            DropTable("dbo.EDSkinTestResultDatas");
            DropTable("dbo.EDSkinTestResults");
            DropTable("dbo.EDJointConsultationForApprovalOfSurgeryDatas");
            DropTable("dbo.EDJointConsultationForApprovalOfSurgeries");
            DropTable("dbo.EDConsultationDrugWithAnAsteriskMarks");
        }
    }
}
