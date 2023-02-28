namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newtablecovid19takeofcare : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IPDTakeCareOfPatientsWithCovid19Assessment",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Version = c.Int(nullable: false),
                        VisitId = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IPDs", t => t.VisitId)
                .Index(t => t.VisitId);
            
            CreateTable(
                "dbo.IPDTakeCareOfPatientsWithCovid19Recomment",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        HandOverAt = c.DateTime(),
                        HandOverBy = c.String(),
                        ReceivingAt = c.DateTime(),
                        ReceivingBy = c.String(),
                        Version = c.Int(nullable: false),
                        VisitId = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IPDs", t => t.VisitId)
                .Index(t => t.VisitId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IPDTakeCareOfPatientsWithCovid19Recomment", "VisitId", "dbo.IPDs");
            DropForeignKey("dbo.IPDTakeCareOfPatientsWithCovid19Assessment", "VisitId", "dbo.IPDs");
            DropIndex("dbo.IPDTakeCareOfPatientsWithCovid19Recomment", new[] { "VisitId" });
            DropIndex("dbo.IPDTakeCareOfPatientsWithCovid19Assessment", new[] { "VisitId" });
            DropTable("dbo.IPDTakeCareOfPatientsWithCovid19Recomment");
            DropTable("dbo.IPDTakeCareOfPatientsWithCovid19Assessment");
        }
    }
}
