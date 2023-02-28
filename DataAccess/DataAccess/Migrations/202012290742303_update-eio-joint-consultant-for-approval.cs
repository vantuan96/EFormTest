namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateeiojointconsultantforapproval : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.EDJointConsultationForApprovalOfSurgeries", newName: "EIOJointConsultationForApprovalOfSurgeries");
            RenameTable(name: "dbo.EDJointConsultationForApprovalOfSurgeryDatas", newName: "EIOJointConsultationForApprovalOfSurgeryDatas");
            RenameColumn(table: "dbo.EIOJointConsultationForApprovalOfSurgeryDatas", name: "EDJointConsultationForApprovalOfSurgeryId", newName: "EIOJointConsultationForApprovalOfSurgeryId");
            RenameIndex(table: "dbo.EIOJointConsultationForApprovalOfSurgeryDatas", name: "IX_EDJointConsultationForApprovalOfSurgeryId", newName: "IX_EIOJointConsultationForApprovalOfSurgeryId");
            AddColumn("dbo.EIOJointConsultationForApprovalOfSurgeries", "VisitId", c => c.Guid());
            AddColumn("dbo.EIOJointConsultationForApprovalOfSurgeries", "VisitTypeGroupCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EIOJointConsultationForApprovalOfSurgeries", "VisitTypeGroupCode");
            DropColumn("dbo.EIOJointConsultationForApprovalOfSurgeries", "VisitId");
            RenameIndex(table: "dbo.EIOJointConsultationForApprovalOfSurgeryDatas", name: "IX_EIOJointConsultationForApprovalOfSurgeryId", newName: "IX_EDJointConsultationForApprovalOfSurgeryId");
            RenameColumn(table: "dbo.EIOJointConsultationForApprovalOfSurgeryDatas", name: "EIOJointConsultationForApprovalOfSurgeryId", newName: "EDJointConsultationForApprovalOfSurgeryId");
            RenameTable(name: "dbo.EIOJointConsultationForApprovalOfSurgeryDatas", newName: "EDJointConsultationForApprovalOfSurgeryDatas");
            RenameTable(name: "dbo.EIOJointConsultationForApprovalOfSurgeries", newName: "EDJointConsultationForApprovalOfSurgeries");
        }
    }
}
