namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateexaminationnote : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OPDOutpatientExaminationNotes",
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
                        ExaminationTime = c.DateTime(),
                        Service = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OPDOutpatientExaminationNoteDatas",
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
                        OPDOutpatientExaminationNoteId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OPDOutpatientExaminationNotes", t => t.OPDOutpatientExaminationNoteId)
                .Index(t => t.OPDOutpatientExaminationNoteId);
            
            AddColumn("dbo.Customers", "WorkPlace", c => c.String());
            AddColumn("dbo.OPDs", "OPDOutpatientExaminationNoteId", c => c.Guid());
            AddColumn("dbo.OPDInitialAssessmentForOnGoings", "AdmittedDate", c => c.DateTime());
            AddColumn("dbo.Rooms", "Value", c => c.String());
            AddColumn("dbo.Rooms", "Service", c => c.String());
            AddColumn("dbo.OPDInitialAssessmentForShortTerms", "AdmittedDate", c => c.DateTime());
            AddColumn("dbo.Sites", "Location", c => c.String());
            AddColumn("dbo.Sites", "Province", c => c.String());
            CreateIndex("dbo.OPDs", "OPDOutpatientExaminationNoteId");
            AddForeignKey("dbo.OPDs", "OPDOutpatientExaminationNoteId", "dbo.OPDOutpatientExaminationNotes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OPDs", "OPDOutpatientExaminationNoteId", "dbo.OPDOutpatientExaminationNotes");
            DropForeignKey("dbo.OPDOutpatientExaminationNoteDatas", "OPDOutpatientExaminationNoteId", "dbo.OPDOutpatientExaminationNotes");
            DropIndex("dbo.OPDOutpatientExaminationNoteDatas", new[] { "OPDOutpatientExaminationNoteId" });
            DropIndex("dbo.OPDs", new[] { "OPDOutpatientExaminationNoteId" });
            DropColumn("dbo.Sites", "Province");
            DropColumn("dbo.Sites", "Location");
            DropColumn("dbo.OPDInitialAssessmentForShortTerms", "AdmittedDate");
            DropColumn("dbo.Rooms", "Service");
            DropColumn("dbo.Rooms", "Value");
            DropColumn("dbo.OPDInitialAssessmentForOnGoings", "AdmittedDate");
            DropColumn("dbo.OPDs", "OPDOutpatientExaminationNoteId");
            DropColumn("dbo.Customers", "WorkPlace");
            DropTable("dbo.OPDOutpatientExaminationNoteDatas");
            DropTable("dbo.OPDOutpatientExaminationNotes");
        }
    }
}
