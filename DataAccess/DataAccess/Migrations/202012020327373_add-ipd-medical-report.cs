namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addipdmedicalreport : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IPDDischargeMedicalReports",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                        PhysicianInChargeId = c.Guid(),
                        DeptHeadId = c.Guid(),
                        DirectorId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.DeptHeadId)
                .ForeignKey("dbo.Users", t => t.DirectorId)
                .ForeignKey("dbo.Users", t => t.PhysicianInChargeId)
                .Index(t => t.PhysicianInChargeId)
                .Index(t => t.DeptHeadId)
                .Index(t => t.DirectorId);
            
            AddColumn("dbo.IPDs", "IPDDischargeMedicalReportId", c => c.Guid());
            CreateIndex("dbo.IPDs", "IPDDischargeMedicalReportId");
            AddForeignKey("dbo.IPDs", "IPDDischargeMedicalReportId", "dbo.IPDDischargeMedicalReports", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IPDs", "IPDDischargeMedicalReportId", "dbo.IPDDischargeMedicalReports");
            DropForeignKey("dbo.IPDDischargeMedicalReports", "PhysicianInChargeId", "dbo.Users");
            DropForeignKey("dbo.IPDDischargeMedicalReports", "DirectorId", "dbo.Users");
            DropForeignKey("dbo.IPDDischargeMedicalReports", "DeptHeadId", "dbo.Users");
            DropIndex("dbo.IPDs", new[] { "IPDDischargeMedicalReportId" });
            DropIndex("dbo.IPDDischargeMedicalReports", new[] { "DirectorId" });
            DropIndex("dbo.IPDDischargeMedicalReports", new[] { "DeptHeadId" });
            DropIndex("dbo.IPDDischargeMedicalReports", new[] { "PhysicianInChargeId" });
            DropColumn("dbo.IPDs", "IPDDischargeMedicalReportId");
            DropTable("dbo.IPDDischargeMedicalReports");
        }
    }
}
