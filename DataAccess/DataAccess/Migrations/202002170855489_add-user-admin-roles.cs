namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adduseradminroles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdminRoles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.UserAdminRoles",
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
                        UserId = c.Guid(nullable: false),
                        AdminRoleId = c.Int(nullable: false),
                        AdminRole_RoleId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AdminRoles", t => t.AdminRole_RoleId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.AdminRole_RoleId);
            
            AddColumn("dbo.Users", "IsAdminUser", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserAdminRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserAdminRoles", "AdminRole_RoleId", "dbo.AdminRoles");
            DropIndex("dbo.UserAdminRoles", new[] { "AdminRole_RoleId" });
            DropIndex("dbo.UserAdminRoles", new[] { "UserId" });
            DropColumn("dbo.Users", "IsAdminUser");
            DropTable("dbo.UserAdminRoles");
            DropTable("dbo.AdminRoles");
        }
    }
}
