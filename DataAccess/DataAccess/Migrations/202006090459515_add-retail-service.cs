namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addretailservice : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EDAssessmentForRetailServicePatients",
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
                        TriageDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EDAssessmentForRetailServicePatientDatas",
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
                        EDAssessmentForRetailServicePatientId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EDAssessmentForRetailServicePatients", t => t.EDAssessmentForRetailServicePatientId)
                .Index(t => t.EDAssessmentForRetailServicePatientId);
            
            AddColumn("dbo.EDs", "IsRetailService", c => c.Boolean(nullable: false));
            AddColumn("dbo.EDs", "EDAssessmentForRetailServicePatientId", c => c.Guid());
            CreateIndex("dbo.EDs", "EDAssessmentForRetailServicePatientId");
            AddForeignKey("dbo.EDs", "EDAssessmentForRetailServicePatientId", "dbo.EDAssessmentForRetailServicePatients", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EDs", "EDAssessmentForRetailServicePatientId", "dbo.EDAssessmentForRetailServicePatients");
            DropForeignKey("dbo.EDAssessmentForRetailServicePatientDatas", "EDAssessmentForRetailServicePatientId", "dbo.EDAssessmentForRetailServicePatients");
            DropIndex("dbo.EDAssessmentForRetailServicePatientDatas", new[] { "EDAssessmentForRetailServicePatientId" });
            DropIndex("dbo.EDs", new[] { "EDAssessmentForRetailServicePatientId" });
            DropColumn("dbo.EDs", "EDAssessmentForRetailServicePatientId");
            DropColumn("dbo.EDs", "IsRetailService");
            DropTable("dbo.EDAssessmentForRetailServicePatientDatas");
            DropTable("dbo.EDAssessmentForRetailServicePatients");
        }
    }
}
