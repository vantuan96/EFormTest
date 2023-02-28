namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_tbl_IpdSumOf15DayTreatMemnt_and_tbl_ipd_MedicationHistory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IPDMedicationHistories", "PharmacistConfirmId", c => c.Guid());
            AddColumn("dbo.IPDMedicationHistories", "DoctorConfirmId", c => c.Guid());
            AddColumn("dbo.IPDSummaryOf15DayTreatment", "HeadOfDepartmentConfirmId", c => c.Guid());
            AddColumn("dbo.IPDSummaryOf15DayTreatment", "PhysicianConfirmId", c => c.Guid());
            DropColumn("dbo.IPDMedicationHistories", "PharmacistConfirm");
            DropColumn("dbo.IPDMedicationHistories", "DoctorConfirm");
            DropColumn("dbo.IPDSummaryOf15DayTreatment", "HeadOfDepartmentConfirm");
            DropColumn("dbo.IPDSummaryOf15DayTreatment", "PhysicianConfirm");
        }
        
        public override void Down()
        {
            AddColumn("dbo.IPDSummaryOf15DayTreatment", "PhysicianConfirm", c => c.String());
            AddColumn("dbo.IPDSummaryOf15DayTreatment", "HeadOfDepartmentConfirm", c => c.String());
            AddColumn("dbo.IPDMedicationHistories", "DoctorConfirm", c => c.String());
            AddColumn("dbo.IPDMedicationHistories", "PharmacistConfirm", c => c.String());
            DropColumn("dbo.IPDSummaryOf15DayTreatment", "PhysicianConfirmId");
            DropColumn("dbo.IPDSummaryOf15DayTreatment", "HeadOfDepartmentConfirmId");
            DropColumn("dbo.IPDMedicationHistories", "DoctorConfirmId");
            DropColumn("dbo.IPDMedicationHistories", "PharmacistConfirmId");
        }
    }
}
