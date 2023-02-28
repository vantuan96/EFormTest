namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update15092022 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UploadImages",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Title = c.String(),
                        Path = c.String(),
                        Url = c.String(),
                        FileType = c.String(),
                        FileSize = c.String(),
                        VisitType = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.DiagnosticReportings", "Nurse", c => c.String());
            AddColumn("dbo.DiagnosticReportings", "Area", c => c.String());
            AddColumn("dbo.Forms", "Version", c => c.Int());
            AddColumn("dbo.Forms", "Time", c => c.Int());
            AddColumn("dbo.Forms", "Ispermission", c => c.Boolean(nullable: false));
            AddColumn("dbo.OPDs", "IsHasFallRiskScreening", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OPDs", "IsHasFallRiskScreening");
            DropColumn("dbo.Forms", "Ispermission");
            DropColumn("dbo.Forms", "Time");
            DropColumn("dbo.Forms", "Version");
            DropColumn("dbo.DiagnosticReportings", "Area");
            DropColumn("dbo.DiagnosticReportings", "Nurse");
            DropTable("dbo.UploadImages");
        }
    }
}
