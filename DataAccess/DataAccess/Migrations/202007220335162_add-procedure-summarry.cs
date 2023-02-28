namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addproceduresummarry : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OPDProcedureSumarryDatas",
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
                        OPDProcedureSumarryId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OPDProcedureSumarries", t => t.OPDProcedureSumarryId)
                .Index(t => t.OPDProcedureSumarryId);
            
            CreateTable(
                "dbo.OPDProcedureSumarries",
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
                        ProcedureTime = c.DateTime(),
                        ProcedureDoctorId = c.Guid(),
                        VisitId = c.Guid(),
                        VisitTypeGroupCode = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.ProcedureDoctorId)
                .Index(t => t.ProcedureDoctorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OPDProcedureSumarries", "ProcedureDoctorId", "dbo.Users");
            DropForeignKey("dbo.OPDProcedureSumarryDatas", "OPDProcedureSumarryId", "dbo.OPDProcedureSumarries");
            DropIndex("dbo.OPDProcedureSumarries", new[] { "ProcedureDoctorId" });
            DropIndex("dbo.OPDProcedureSumarryDatas", new[] { "OPDProcedureSumarryId" });
            DropTable("dbo.OPDProcedureSumarries");
            DropTable("dbo.OPDProcedureSumarryDatas");
        }
    }
}
