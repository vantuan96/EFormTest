namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initendoscopy : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EOCs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CustomerId = c.Guid(),
                        AdmittedDate = c.DateTime(nullable: false),
                        DischargeDate = c.DateTime(),
                        VisitCode = c.String(maxLength: 255, unicode: false),
                        RecordCode = c.String(maxLength: 100, unicode: false),
                        HealthInsuranceNumber = c.String(),
                        StartHealthInsuranceDate = c.DateTime(),
                        ExpireHealthInsuranceDate = c.DateTime(),
                        IsTransfer = c.Boolean(nullable: false),
                        TransferFromId = c.Guid(),
                        StatusId = c.Guid(),
                        SiteId = c.Guid(),
                        SpecialtyId = c.Guid(),
                        PrimaryDoctorId = c.Guid(),
                        CurrentDoctorId = c.Guid(),
                        CurrentNurseId = c.Guid(),
                        EFormVisitId = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CurrentDoctorId)
                .ForeignKey("dbo.Users", t => t.CurrentNurseId)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .ForeignKey("dbo.Users", t => t.PrimaryDoctorId)
                .ForeignKey("dbo.Sites", t => t.SiteId)
                .ForeignKey("dbo.Specialties", t => t.SpecialtyId)
                .ForeignKey("dbo.EDStatus", t => t.StatusId)
                .Index(t => t.CustomerId)
                .Index(t => t.AdmittedDate)
                .Index(t => t.VisitCode)
                .Index(t => t.RecordCode)
                .Index(t => t.StatusId)
                .Index(t => t.SiteId)
                .Index(t => t.SpecialtyId)
                .Index(t => t.PrimaryDoctorId)
                .Index(t => t.CurrentDoctorId)
                .Index(t => t.CurrentNurseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EOCs", "StatusId", "dbo.EDStatus");
            DropForeignKey("dbo.EOCs", "SpecialtyId", "dbo.Specialties");
            DropForeignKey("dbo.EOCs", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.EOCs", "PrimaryDoctorId", "dbo.Users");
            DropForeignKey("dbo.EOCs", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.EOCs", "CurrentNurseId", "dbo.Users");
            DropForeignKey("dbo.EOCs", "CurrentDoctorId", "dbo.Users");
            DropIndex("dbo.EOCs", new[] { "CurrentNurseId" });
            DropIndex("dbo.EOCs", new[] { "CurrentDoctorId" });
            DropIndex("dbo.EOCs", new[] { "PrimaryDoctorId" });
            DropIndex("dbo.EOCs", new[] { "SpecialtyId" });
            DropIndex("dbo.EOCs", new[] { "SiteId" });
            DropIndex("dbo.EOCs", new[] { "StatusId" });
            DropIndex("dbo.EOCs", new[] { "RecordCode" });
            DropIndex("dbo.EOCs", new[] { "VisitCode" });
            DropIndex("dbo.EOCs", new[] { "AdmittedDate" });
            DropIndex("dbo.EOCs", new[] { "CustomerId" });
            DropTable("dbo.EOCs");
        }
    }
}
