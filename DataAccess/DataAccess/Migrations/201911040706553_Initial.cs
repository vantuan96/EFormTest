namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PID = c.String(),
                        Fullname = c.String(),
                        Gender = c.String(),
                        DateOfBirth = c.DateTime(),
                        Address = c.String(),
                        Nationality = c.String(),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTime(),
                        DeletedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EDs",
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
                        SiteId = c.Int(),
                        CustomerId = c.Int(),
                        EmergencyTriageRecordId = c.Int(),
                        InnitialAssessmentId = c.Int(),
                        PatientProgressNoteId = c.Int(),
                        PreOperativeProcedureHandoverChecklistId = c.Int(),
                        Customer_Id = c.Guid(),
                        EmergencyTriageRecord_Id = c.Guid(),
                        InnitialAssessment_Id = c.Guid(),
                        PatientProgressNote_Id = c.Guid(),
                        PreOperativeProcedureHandoverChecklist_Id = c.Guid(),
                        Site_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .ForeignKey("dbo.EmergencyTriageRecords", t => t.EmergencyTriageRecord_Id)
                .ForeignKey("dbo.InnitialAssessments", t => t.InnitialAssessment_Id)
                .ForeignKey("dbo.PatientProgressNotes", t => t.PatientProgressNote_Id)
                .ForeignKey("dbo.PreOperativeProcedureHandoverChecklists", t => t.PreOperativeProcedureHandoverChecklist_Id)
                .ForeignKey("dbo.Sites", t => t.Site_Id)
                .Index(t => t.Customer_Id)
                .Index(t => t.EmergencyTriageRecord_Id)
                .Index(t => t.InnitialAssessment_Id)
                .Index(t => t.PatientProgressNote_Id)
                .Index(t => t.PreOperativeProcedureHandoverChecklist_Id)
                .Index(t => t.Site_Id);
            
            CreateTable(
                "dbo.EmergencyTriageRecords",
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
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EmergencyTriageRecordDatas",
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
                        QuestionCode = c.String(),
                        AnswerCode = c.String(),
                        Value = c.String(),
                        EmergencyTriageRecordId = c.Int(),
                        EmergencyTriageRecord_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmergencyTriageRecords", t => t.EmergencyTriageRecord_Id)
                .Index(t => t.EmergencyTriageRecord_Id);
            
            CreateTable(
                "dbo.Orders",
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
                        Dir = c.String(),
                        Dr = c.String(),
                        Time = c.Time(precision: 7),
                        RN = c.String(),
                        LabTestImaging = c.String(),
                        POCT = c.String(),
                        TimeNotified = c.Time(precision: 7),
                        DrArrivalTime = c.Time(precision: 7),
                        EmergencyTriageRecordId = c.Int(),
                        EmergencyTriageRecord_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmergencyTriageRecords", t => t.EmergencyTriageRecord_Id)
                .Index(t => t.EmergencyTriageRecord_Id);
            
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
                        EmergencyTriageRecordId = c.Int(),
                        EmergencyTriageRecord_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmergencyTriageRecords", t => t.EmergencyTriageRecord_Id)
                .Index(t => t.EmergencyTriageRecord_Id);
            
            CreateTable(
                "dbo.InnitialAssessments",
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
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InnitialAssessmentDatas",
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
                        QuestionCode = c.String(),
                        AnswerCode = c.String(),
                        Value = c.String(),
                        InnitialAssessmentId = c.Int(),
                        InnitialAssessment_Id = c.Guid(),
                        InnitialAssessment_Id1 = c.Guid(),
                        InnitialAssessment_Id2 = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.InnitialAssessments", t => t.InnitialAssessment_Id)
                .ForeignKey("dbo.InnitialAssessments", t => t.InnitialAssessment_Id1)
                .ForeignKey("dbo.InnitialAssessments", t => t.InnitialAssessment_Id2)
                .Index(t => t.InnitialAssessment_Id)
                .Index(t => t.InnitialAssessment_Id1)
                .Index(t => t.InnitialAssessment_Id2);
            
            CreateTable(
                "dbo.PatientProgressNotes",
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
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PatientProgressNoteDatas",
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
                        DateTime = c.DateTime(),
                        Note = c.String(),
                        Interventions = c.String(),
                        PatientProgressNoteId = c.Int(),
                        PatientProgressNote_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PatientProgressNotes", t => t.PatientProgressNote_Id)
                .Index(t => t.PatientProgressNote_Id);
            
            CreateTable(
                "dbo.PreOperativeProcedureHandoverChecklists",
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
                        DateTimeHandover = c.DateTime(),
                        WardNurse = c.String(),
                        EscortNurse = c.String(),
                        ReceivingNurse = c.String(),
                        DateTimeTest = c.DateTime(),
                        ScrubNurse = c.String(),
                        CirculatingNurse = c.String(),
                        Surgeon = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PreOperativeProcedureHandoverChecklistDatas",
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
                        PreOperativeProcedureHandoverChecklistId = c.Int(),
                        PreOperativeProcedureHandoverChecklist_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PreOperativeProcedureHandoverChecklists", t => t.PreOperativeProcedureHandoverChecklist_Id)
                .Index(t => t.PreOperativeProcedureHandoverChecklist_Id);
            
            CreateTable(
                "dbo.Sites",
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
                        Name = c.String(),
                        Code = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MasterDatas",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ViName = c.String(),
                        EngName = c.String(),
                        Code = c.String(),
                        Data_type = c.String(),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTime(),
                        DeletedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserTrackings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Username = c.String(),
                        Action = c.String(),
                        BeforeAction = c.String(),
                        AfterAction = c.String(),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTime(),
                        DeletedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EDs", "Site_Id", "dbo.Sites");
            DropForeignKey("dbo.PreOperativeProcedureHandoverChecklistDatas", "PreOperativeProcedureHandoverChecklist_Id", "dbo.PreOperativeProcedureHandoverChecklists");
            DropForeignKey("dbo.EDs", "PreOperativeProcedureHandoverChecklist_Id", "dbo.PreOperativeProcedureHandoverChecklists");
            DropForeignKey("dbo.PatientProgressNoteDatas", "PatientProgressNote_Id", "dbo.PatientProgressNotes");
            DropForeignKey("dbo.EDs", "PatientProgressNote_Id", "dbo.PatientProgressNotes");
            DropForeignKey("dbo.InnitialAssessmentDatas", "InnitialAssessment_Id2", "dbo.InnitialAssessments");
            DropForeignKey("dbo.InnitialAssessmentDatas", "InnitialAssessment_Id1", "dbo.InnitialAssessments");
            DropForeignKey("dbo.InnitialAssessmentDatas", "InnitialAssessment_Id", "dbo.InnitialAssessments");
            DropForeignKey("dbo.EDs", "InnitialAssessment_Id", "dbo.InnitialAssessments");
            DropForeignKey("dbo.VitalSigns", "EmergencyTriageRecord_Id", "dbo.EmergencyTriageRecords");
            DropForeignKey("dbo.Orders", "EmergencyTriageRecord_Id", "dbo.EmergencyTriageRecords");
            DropForeignKey("dbo.EmergencyTriageRecordDatas", "EmergencyTriageRecord_Id", "dbo.EmergencyTriageRecords");
            DropForeignKey("dbo.EDs", "EmergencyTriageRecord_Id", "dbo.EmergencyTriageRecords");
            DropForeignKey("dbo.EDs", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.PreOperativeProcedureHandoverChecklistDatas", new[] { "PreOperativeProcedureHandoverChecklist_Id" });
            DropIndex("dbo.PatientProgressNoteDatas", new[] { "PatientProgressNote_Id" });
            DropIndex("dbo.InnitialAssessmentDatas", new[] { "InnitialAssessment_Id2" });
            DropIndex("dbo.InnitialAssessmentDatas", new[] { "InnitialAssessment_Id1" });
            DropIndex("dbo.InnitialAssessmentDatas", new[] { "InnitialAssessment_Id" });
            DropIndex("dbo.VitalSigns", new[] { "EmergencyTriageRecord_Id" });
            DropIndex("dbo.Orders", new[] { "EmergencyTriageRecord_Id" });
            DropIndex("dbo.EmergencyTriageRecordDatas", new[] { "EmergencyTriageRecord_Id" });
            DropIndex("dbo.EDs", new[] { "Site_Id" });
            DropIndex("dbo.EDs", new[] { "PreOperativeProcedureHandoverChecklist_Id" });
            DropIndex("dbo.EDs", new[] { "PatientProgressNote_Id" });
            DropIndex("dbo.EDs", new[] { "InnitialAssessment_Id" });
            DropIndex("dbo.EDs", new[] { "EmergencyTriageRecord_Id" });
            DropIndex("dbo.EDs", new[] { "Customer_Id" });
            DropTable("dbo.UserTrackings");
            DropTable("dbo.MasterDatas");
            DropTable("dbo.Sites");
            DropTable("dbo.PreOperativeProcedureHandoverChecklistDatas");
            DropTable("dbo.PreOperativeProcedureHandoverChecklists");
            DropTable("dbo.PatientProgressNoteDatas");
            DropTable("dbo.PatientProgressNotes");
            DropTable("dbo.InnitialAssessmentDatas");
            DropTable("dbo.InnitialAssessments");
            DropTable("dbo.VitalSigns");
            DropTable("dbo.Orders");
            DropTable("dbo.EmergencyTriageRecordDatas");
            DropTable("dbo.EmergencyTriageRecords");
            DropTable("dbo.EDs");
            DropTable("dbo.Customers");
        }
    }
}
