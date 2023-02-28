namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateorder : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "OPDInitialAssessmentForShortTermId", "dbo.OPDInitialAssessmentForShortTerms");
            DropForeignKey("dbo.Orders", "OPDId", "dbo.OPDs");
            DropForeignKey("dbo.Orders", "OPDInitialAssessmentForOnGoingId", "dbo.OPDInitialAssessmentForOnGoings");
            DropForeignKey("dbo.VitalSigns", "EmergencyTriageRecordId", "dbo.EmergencyTriageRecords");
            DropIndex("dbo.Orders", new[] { "OPDInitialAssessmentForShortTermId" });
            DropIndex("dbo.Orders", new[] { "OPDInitialAssessmentForOnGoingId" });
            DropIndex("dbo.Orders", new[] { "OPDId" });
            DropIndex("dbo.VitalSigns", new[] { "EmergencyTriageRecordId" });
            AddColumn("dbo.Orders", "OrderType", c => c.String());
            AddColumn("dbo.Orders", "VisitId", c => c.Guid());
            DropColumn("dbo.Orders", "OPDInitialAssessmentForShortTermId");
            DropColumn("dbo.Orders", "OPDInitialAssessmentForOnGoingId");
            DropColumn("dbo.Orders", "OPDId");
            DropTable("dbo.VitalSigns");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.VitalSigns",
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
                    Time = c.Time(precision: 7),
                    RR = c.Double(),
                    SpO2 = c.Double(),
                    HR = c.Double(),
                    BP = c.Double(),
                    T = c.Double(),
                    GCS = c.Double(),
                    Pain = c.Double(),
                    ATSScale = c.String(),
                    ReAssessmentIntervention = c.String(),
                    RN = c.String(),
                    EmergencyTriageRecordId = c.Guid(),
                })
                .PrimaryKey(t => t.Id);

            AddColumn("dbo.Orders", "OPDId", c => c.Guid());
            AddColumn("dbo.Orders", "OPDInitialAssessmentForOnGoingId", c => c.Guid());
            AddColumn("dbo.Orders", "OPDInitialAssessmentForShortTermId", c => c.Guid());
            DropColumn("dbo.Orders", "VisitId");
            DropColumn("dbo.Orders", "OrderType");
            CreateIndex("dbo.VitalSigns", "EmergencyTriageRecordId");
            CreateIndex("dbo.Orders", "OPDId");
            CreateIndex("dbo.Orders", "OPDInitialAssessmentForOnGoingId");
            CreateIndex("dbo.Orders", "OPDInitialAssessmentForShortTermId");
            AddForeignKey("dbo.VitalSigns", "EmergencyTriageRecordId", "dbo.EmergencyTriageRecords", "Id");
            AddForeignKey("dbo.Orders", "OPDInitialAssessmentForOnGoingId", "dbo.OPDInitialAssessmentForOnGoings", "Id");
            AddForeignKey("dbo.Orders", "OPDId", "dbo.OPDs", "Id");
            AddForeignKey("dbo.Orders", "OPDInitialAssessmentForShortTermId", "dbo.OPDInitialAssessmentForShortTerms", "Id");
        }
    }
}
