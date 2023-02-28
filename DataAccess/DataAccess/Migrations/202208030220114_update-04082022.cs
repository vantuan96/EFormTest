namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update04082022 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SurgeryAndProcedureSummaryV3",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VisitId = c.Guid(),
                        VisitType = c.String(),
                        ProcedureDoctorId = c.Guid(),
                        ProcedureTime = c.DateTime(),
                        Note = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.EDPointOfCareTestingMasterDatas", "Version", c => c.Int(nullable: false));
            AddColumn("dbo.EIOBloodRequestSupplyAndConfirmations", "DoctorConfirmId", c => c.Guid());
            AddColumn("dbo.EIOBloodRequestSupplyAndConfirmations", "DoctorConfirmTime", c => c.DateTime());
            AddColumn("dbo.EIOBloodRequestSupplyAndConfirmations", "Version", c => c.Int(nullable: false));
            AddColumn("dbo.PatientAndFamilyEducations", "Version", c => c.Int(nullable: false));
            CreateIndex("dbo.EIOBloodRequestSupplyAndConfirmations", "DoctorConfirmId");
            AddForeignKey("dbo.EIOBloodRequestSupplyAndConfirmations", "DoctorConfirmId", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EIOBloodRequestSupplyAndConfirmations", "DoctorConfirmId", "dbo.Users");
            DropIndex("dbo.EIOBloodRequestSupplyAndConfirmations", new[] { "DoctorConfirmId" });
            DropColumn("dbo.PatientAndFamilyEducations", "Version");
            DropColumn("dbo.EIOBloodRequestSupplyAndConfirmations", "Version");
            DropColumn("dbo.EIOBloodRequestSupplyAndConfirmations", "DoctorConfirmTime");
            DropColumn("dbo.EIOBloodRequestSupplyAndConfirmations", "DoctorConfirmId");
            DropColumn("dbo.EDPointOfCareTestingMasterDatas", "Version");
            DropTable("dbo.SurgeryAndProcedureSummaryV3");
        }
    }
}
