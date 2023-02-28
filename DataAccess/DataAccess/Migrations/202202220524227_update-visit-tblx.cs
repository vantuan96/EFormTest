namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatevisittblx : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UnlockVips",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CustomerId = c.Guid(),
                        VisitId = c.Guid(),
                        VisitCode = c.String(),
                        RecodeCode = c.String(),
                        PID = c.String(),
                        Type = c.String(),
                        ExpiredAt = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Customers", "IsVip", c => c.Boolean(nullable: false));
            AddColumn("dbo.EDs", "PatientLocationCode", c => c.String());
            AddColumn("dbo.EDs", "VisitGroupType", c => c.String());
            AddColumn("dbo.EDs", "AreaName", c => c.String());
            AddColumn("dbo.EDs", "VisitType", c => c.String());
            AddColumn("dbo.EDs", "HospitalCode", c => c.String());
            AddColumn("dbo.EDs", "DoctorAD", c => c.String());
            AddColumn("dbo.EDs", "PatientLocationId", c => c.Guid());
            AddColumn("dbo.EDs", "PatientVisitId", c => c.Guid());
            AddColumn("dbo.EDs", "ActualVisitDate", c => c.DateTime());
            AddColumn("dbo.EDs", "PrimaryNurseId", c => c.Guid());
            AddColumn("dbo.EOCs", "PatientLocationCode", c => c.String());
            AddColumn("dbo.EOCs", "VisitGroupType", c => c.String());
            AddColumn("dbo.EOCs", "AreaName", c => c.String());
            AddColumn("dbo.EOCs", "VisitType", c => c.String());
            AddColumn("dbo.EOCs", "HospitalCode", c => c.String());
            AddColumn("dbo.EOCs", "DoctorAD", c => c.String());
            AddColumn("dbo.EOCs", "PatientLocationId", c => c.Guid());
            AddColumn("dbo.EOCs", "PatientVisitId", c => c.Guid());
            AddColumn("dbo.EOCs", "ActualVisitDate", c => c.DateTime());
            AddColumn("dbo.IPDs", "PatientLocationCode", c => c.String());
            AddColumn("dbo.IPDs", "VisitGroupType", c => c.String());
            AddColumn("dbo.IPDs", "AreaName", c => c.String());
            AddColumn("dbo.IPDs", "VisitType", c => c.String());
            AddColumn("dbo.IPDs", "HospitalCode", c => c.String());
            AddColumn("dbo.IPDs", "DoctorAD", c => c.String());
            AddColumn("dbo.IPDs", "PatientLocationId", c => c.Guid());
            AddColumn("dbo.IPDs", "PatientVisitId", c => c.Guid());
            AddColumn("dbo.IPDs", "ActualVisitDate", c => c.DateTime());
            AddColumn("dbo.OPDOutpatientExaminationNotes", "IsConsultation", c => c.Boolean());
            AddColumn("dbo.OPDs", "PatientLocationCode", c => c.String());
            AddColumn("dbo.OPDs", "VisitGroupType", c => c.String());
            AddColumn("dbo.OPDs", "AreaName", c => c.String());
            AddColumn("dbo.OPDs", "VisitType", c => c.String());
            AddColumn("dbo.OPDs", "HospitalCode", c => c.String());
            AddColumn("dbo.OPDs", "DoctorAD", c => c.String());
            AddColumn("dbo.OPDs", "PatientLocationId", c => c.Guid());
            AddColumn("dbo.OPDs", "PatientVisitId", c => c.Guid());
            AddColumn("dbo.OPDs", "ActualVisitDate", c => c.DateTime());
            CreateIndex("dbo.EDs", "PrimaryNurseId");
            CreateIndex("dbo.IPDs", "AdmittedDate");
            AddForeignKey("dbo.EDs", "PrimaryNurseId", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EDs", "PrimaryNurseId", "dbo.Users");
            DropIndex("dbo.IPDs", new[] { "AdmittedDate" });
            DropIndex("dbo.EDs", new[] { "PrimaryNurseId" });
            DropColumn("dbo.OPDs", "ActualVisitDate");
            DropColumn("dbo.OPDs", "PatientVisitId");
            DropColumn("dbo.OPDs", "PatientLocationId");
            DropColumn("dbo.OPDs", "DoctorAD");
            DropColumn("dbo.OPDs", "HospitalCode");
            DropColumn("dbo.OPDs", "VisitType");
            DropColumn("dbo.OPDs", "AreaName");
            DropColumn("dbo.OPDs", "VisitGroupType");
            DropColumn("dbo.OPDs", "PatientLocationCode");
            DropColumn("dbo.OPDOutpatientExaminationNotes", "IsConsultation");
            DropColumn("dbo.IPDs", "ActualVisitDate");
            DropColumn("dbo.IPDs", "PatientVisitId");
            DropColumn("dbo.IPDs", "PatientLocationId");
            DropColumn("dbo.IPDs", "DoctorAD");
            DropColumn("dbo.IPDs", "HospitalCode");
            DropColumn("dbo.IPDs", "VisitType");
            DropColumn("dbo.IPDs", "AreaName");
            DropColumn("dbo.IPDs", "VisitGroupType");
            DropColumn("dbo.IPDs", "PatientLocationCode");
            DropColumn("dbo.EOCs", "ActualVisitDate");
            DropColumn("dbo.EOCs", "PatientVisitId");
            DropColumn("dbo.EOCs", "PatientLocationId");
            DropColumn("dbo.EOCs", "DoctorAD");
            DropColumn("dbo.EOCs", "HospitalCode");
            DropColumn("dbo.EOCs", "VisitType");
            DropColumn("dbo.EOCs", "AreaName");
            DropColumn("dbo.EOCs", "VisitGroupType");
            DropColumn("dbo.EOCs", "PatientLocationCode");
            DropColumn("dbo.EDs", "PrimaryNurseId");
            DropColumn("dbo.EDs", "ActualVisitDate");
            DropColumn("dbo.EDs", "PatientVisitId");
            DropColumn("dbo.EDs", "PatientLocationId");
            DropColumn("dbo.EDs", "DoctorAD");
            DropColumn("dbo.EDs", "HospitalCode");
            DropColumn("dbo.EDs", "VisitType");
            DropColumn("dbo.EDs", "AreaName");
            DropColumn("dbo.EDs", "VisitGroupType");
            DropColumn("dbo.EDs", "PatientLocationCode");
            DropColumn("dbo.Customers", "IsVip");
            DropTable("dbo.UnlockVips");
        }
    }
}
