namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class opdassessmentforretailservice : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.EDAssessmentForRetailServicePatientDatas", newName: "EIOAssessmentForRetailServicePatientDatas");
            RenameTable(name: "dbo.EDAssessmentForRetailServicePatients", newName: "EIOAssessmentForRetailServicePatients");
            RenameTable(name: "dbo.EDStandingOrderForRetailServices", newName: "EIOStandingOrderForRetailServices");
            AddColumn("dbo.OPDs", "IsRetailService", c => c.Boolean(nullable: false));
            AddColumn("dbo.OPDs", "EIOAssessmentForRetailServicePatientId", c => c.Guid());
            AddColumn("dbo.OPDs", "EIOStandingOrderForRetailServiceId", c => c.Guid());
            CreateIndex("dbo.OPDs", "EIOAssessmentForRetailServicePatientId");
            CreateIndex("dbo.OPDs", "EIOStandingOrderForRetailServiceId");
            AddForeignKey("dbo.OPDs", "EIOAssessmentForRetailServicePatientId", "dbo.EIOAssessmentForRetailServicePatients", "Id");
            AddForeignKey("dbo.OPDs", "EIOStandingOrderForRetailServiceId", "dbo.EIOStandingOrderForRetailServices", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OPDs", "EIOStandingOrderForRetailServiceId", "dbo.EIOStandingOrderForRetailServices");
            DropForeignKey("dbo.OPDs", "EIOAssessmentForRetailServicePatientId", "dbo.EIOAssessmentForRetailServicePatients");
            DropIndex("dbo.OPDs", new[] { "EIOStandingOrderForRetailServiceId" });
            DropIndex("dbo.OPDs", new[] { "EIOAssessmentForRetailServicePatientId" });
            DropColumn("dbo.OPDs", "EIOStandingOrderForRetailServiceId");
            DropColumn("dbo.OPDs", "EIOAssessmentForRetailServicePatientId");
            DropColumn("dbo.OPDs", "IsRetailService");
            RenameTable(name: "dbo.EIOStandingOrderForRetailServices", newName: "EDStandingOrderForRetailServices");
            RenameTable(name: "dbo.EIOAssessmentForRetailServicePatients", newName: "EDAssessmentForRetailServicePatients");
            RenameTable(name: "dbo.EIOAssessmentForRetailServicePatientDatas", newName: "EDAssessmentForRetailServicePatientDatas");
        }
    }
}
