namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeroominopdinitialassessment : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.OPDInitialAssessmentForOnGoings", name: "RoomID", newName: "Room_Id");
            RenameColumn(table: "dbo.OPDInitialAssessmentForShortTerms", name: "RoomID", newName: "Room_Id");
            RenameIndex(table: "dbo.OPDInitialAssessmentForOnGoings", name: "IX_RoomID", newName: "IX_Room_Id");
            RenameIndex(table: "dbo.OPDInitialAssessmentForShortTerms", name: "IX_RoomID", newName: "IX_Room_Id");
            AddColumn("dbo.MasterDatas", "Clinic", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MasterDatas", "Clinic");
            RenameIndex(table: "dbo.OPDInitialAssessmentForShortTerms", name: "IX_Room_Id", newName: "IX_RoomID");
            RenameIndex(table: "dbo.OPDInitialAssessmentForOnGoings", name: "IX_Room_Id", newName: "IX_RoomID");
            RenameColumn(table: "dbo.OPDInitialAssessmentForShortTerms", name: "Room_Id", newName: "RoomID");
            RenameColumn(table: "dbo.OPDInitialAssessmentForOnGoings", name: "Room_Id", newName: "RoomID");
        }
    }
}
