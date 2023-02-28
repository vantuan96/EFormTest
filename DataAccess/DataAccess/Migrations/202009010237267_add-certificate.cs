namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcertificate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EDInjuryCertificates",
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
                        DoctorTime = c.DateTime(),
                        DoctorId = c.Guid(),
                        HeadOfDeptTime = c.DateTime(),
                        HeadOfDeptId = c.Guid(),
                        DirectorTime = c.DateTime(),
                        DirectorId = c.Guid(),
                        VisitId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.DirectorId)
                .ForeignKey("dbo.Users", t => t.DoctorId)
                .ForeignKey("dbo.Users", t => t.HeadOfDeptId)
                .Index(t => t.DoctorId)
                .Index(t => t.HeadOfDeptId)
                .Index(t => t.DirectorId);
            
            AddColumn("dbo.Customers", "IdentificationCard", c => c.String());
            AddColumn("dbo.Customers", "IssueDate", c => c.DateTime());
            AddColumn("dbo.Customers", "IssuePlace", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EDInjuryCertificates", "HeadOfDeptId", "dbo.Users");
            DropForeignKey("dbo.EDInjuryCertificates", "DoctorId", "dbo.Users");
            DropForeignKey("dbo.EDInjuryCertificates", "DirectorId", "dbo.Users");
            DropIndex("dbo.EDInjuryCertificates", new[] { "DirectorId" });
            DropIndex("dbo.EDInjuryCertificates", new[] { "HeadOfDeptId" });
            DropIndex("dbo.EDInjuryCertificates", new[] { "DoctorId" });
            DropColumn("dbo.Customers", "IssuePlace");
            DropColumn("dbo.Customers", "IssueDate");
            DropColumn("dbo.Customers", "IdentificationCard");
            DropTable("dbo.EDInjuryCertificates");
        }
    }
}
