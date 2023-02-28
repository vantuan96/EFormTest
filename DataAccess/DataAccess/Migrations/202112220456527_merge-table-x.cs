namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mergetablex : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IPDInjuryCertificates",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PID = c.String(nullable: false, maxLength: 10),
                        VisitId = c.Guid(),
                        DoctorTime = c.DateTime(),
                        DoctorId = c.Guid(),
                        HeadOfDeptTime = c.DateTime(),
                        HeadOfDeptId = c.Guid(),
                        DirectorTime = c.DateTime(),
                        DirectorId = c.Guid(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.DirectorId)
                .ForeignKey("dbo.Users", t => t.DoctorId)
                .ForeignKey("dbo.Users", t => t.HeadOfDeptId)
                .Index(t => t.DoctorId)
                .Index(t => t.HeadOfDeptId)
                .Index(t => t.DirectorId);
            
            AddColumn("dbo.ServiceGroups", "IsActive", c => c.Boolean());
            AddColumn("dbo.ServiceGroups", "Type", c => c.String());
            AddColumn("dbo.ServiceGroups", "KeyStruct", c => c.String());
            AddColumn("dbo.ServiceGroups", "HISLastUpdated", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IPDInjuryCertificates", "HeadOfDeptId", "dbo.Users");
            DropForeignKey("dbo.IPDInjuryCertificates", "DoctorId", "dbo.Users");
            DropForeignKey("dbo.IPDInjuryCertificates", "DirectorId", "dbo.Users");
            DropIndex("dbo.IPDInjuryCertificates", new[] { "DirectorId" });
            DropIndex("dbo.IPDInjuryCertificates", new[] { "HeadOfDeptId" });
            DropIndex("dbo.IPDInjuryCertificates", new[] { "DoctorId" });
            DropColumn("dbo.ServiceGroups", "HISLastUpdated");
            DropColumn("dbo.ServiceGroups", "KeyStruct");
            DropColumn("dbo.ServiceGroups", "Type");
            DropColumn("dbo.ServiceGroups", "IsActive");
            DropTable("dbo.IPDInjuryCertificates");
        }
    }
}
