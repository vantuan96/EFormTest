namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtblorderset : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChargePackages",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Code = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ChargePackageServices",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ServiceId = c.Guid(nullable: false),
                        ChargePackageId = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChargePackages", t => t.ChargePackageId, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: true)
                .Index(t => t.ServiceId)
                .Index(t => t.ChargePackageId);
            
            CreateTable(
                "dbo.ChargePackageUsers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        ChargePackageId = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChargePackages", t => t.ChargePackageId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ChargePackageId);
            
            AddColumn("dbo.Services", "ServiceType", c => c.String());
            AddColumn("dbo.ServiceGroups", "ServiceType", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChargePackageUsers", "UserId", "dbo.Users");
            DropForeignKey("dbo.ChargePackageUsers", "ChargePackageId", "dbo.ChargePackages");
            DropForeignKey("dbo.ChargePackageServices", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.ChargePackageServices", "ChargePackageId", "dbo.ChargePackages");
            DropIndex("dbo.ChargePackageUsers", new[] { "ChargePackageId" });
            DropIndex("dbo.ChargePackageUsers", new[] { "UserId" });
            DropIndex("dbo.ChargePackageServices", new[] { "ChargePackageId" });
            DropIndex("dbo.ChargePackageServices", new[] { "ServiceId" });
            DropColumn("dbo.ServiceGroups", "ServiceType");
            DropColumn("dbo.Services", "ServiceType");
            DropTable("dbo.ChargePackageUsers");
            DropTable("dbo.ChargePackageServices");
            DropTable("dbo.ChargePackages");
        }
    }
}
