namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDiagnosticReporting_tbl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DiagnosticReportings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Technique = c.String(),
                        Findings = c.String(),
                        Impression = c.String(),
                        ExamCompleted = c.DateTime(),
                        Status = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DiagnosticReportings");
        }
    }
}
