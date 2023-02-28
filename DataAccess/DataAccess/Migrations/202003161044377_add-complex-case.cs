namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcomplexcase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ComplexOutpatientCaseSummaries",
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
                        MainDiseases = c.String(),
                        Orders = c.String(),
                        CustomerId = c.Guid(),
                        PrimaryDoctorId = c.Guid(),
                        VisitId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .ForeignKey("dbo.Users", t => t.PrimaryDoctorId)
                .Index(t => t.CustomerId)
                .Index(t => t.PrimaryDoctorId);
            
            AddColumn("dbo.Customers", "IsChronic", c => c.Boolean(nullable: false));
            AddColumn("dbo.ICD10", "IsChronic", c => c.Boolean(nullable: false));
            AddColumn("dbo.ICD10", "ClinicId", c => c.Guid());
            CreateIndex("dbo.ICD10", "ClinicId");
            AddForeignKey("dbo.ICD10", "ClinicId", "dbo.Clinics", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ICD10", "ClinicId", "dbo.Clinics");
            DropForeignKey("dbo.ComplexOutpatientCaseSummaries", "PrimaryDoctorId", "dbo.Users");
            DropForeignKey("dbo.ComplexOutpatientCaseSummaries", "CustomerId", "dbo.Customers");
            DropIndex("dbo.ICD10", new[] { "ClinicId" });
            DropIndex("dbo.ComplexOutpatientCaseSummaries", new[] { "PrimaryDoctorId" });
            DropIndex("dbo.ComplexOutpatientCaseSummaries", new[] { "CustomerId" });
            DropColumn("dbo.ICD10", "ClinicId");
            DropColumn("dbo.ICD10", "IsChronic");
            DropColumn("dbo.Customers", "IsChronic");
            DropTable("dbo.ComplexOutpatientCaseSummaries");
        }
    }
}
