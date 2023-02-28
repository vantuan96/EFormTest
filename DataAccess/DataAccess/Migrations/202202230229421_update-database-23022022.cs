namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedatabase23022022 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IPDSurgeryCertificates",
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
                        ProcedureTime = c.DateTime(),
                        ProcedureDoctorId = c.Guid(),
                        DeanConfirmTime = c.DateTime(),
                        DeanId = c.Guid(),
                        DirectorTime = c.DateTime(),
                        DirectorId = c.Guid(),
                        VisitId = c.Guid(),
                        VisitTypeGroupCode = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.DeanId)
                .ForeignKey("dbo.Users", t => t.DirectorId)
                .ForeignKey("dbo.Users", t => t.ProcedureDoctorId)
                .Index(t => t.ProcedureDoctorId)
                .Index(t => t.DeanId)
                .Index(t => t.DirectorId);
            
            CreateTable(
                "dbo.IPDSurgeryCertificateDatas",
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
                        Code = c.String(),
                        Value = c.String(),
                        EnValue = c.String(),
                        IPDSurgeryCertificateId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IPDSurgeryCertificates", t => t.IPDSurgeryCertificateId)
                .Index(t => t.IPDSurgeryCertificateId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IPDSurgeryCertificates", "ProcedureDoctorId", "dbo.Users");
            DropForeignKey("dbo.IPDSurgeryCertificateDatas", "IPDSurgeryCertificateId", "dbo.IPDSurgeryCertificates");
            DropForeignKey("dbo.IPDSurgeryCertificates", "DirectorId", "dbo.Users");
            DropForeignKey("dbo.IPDSurgeryCertificates", "DeanId", "dbo.Users");
            DropIndex("dbo.IPDSurgeryCertificateDatas", new[] { "IPDSurgeryCertificateId" });
            DropIndex("dbo.IPDSurgeryCertificates", new[] { "DirectorId" });
            DropIndex("dbo.IPDSurgeryCertificates", new[] { "DeanId" });
            DropIndex("dbo.IPDSurgeryCertificates", new[] { "ProcedureDoctorId" });
            DropTable("dbo.IPDSurgeryCertificateDatas");
            DropTable("dbo.IPDSurgeryCertificates");
        }
    }
}
