namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeadminroles : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserAdminRoles", "AdminRole_RoleId", "dbo.AdminRoles");
            DropIndex("dbo.UserAdminRoles", new[] { "AdminRole_RoleId" });
            DropColumn("dbo.UserAdminRoles", "AdminRoleId");
            RenameColumn(table: "dbo.UserAdminRoles", name: "AdminRole_RoleId", newName: "AdminRoleId");
            DropPrimaryKey("dbo.AdminRoles");
            DropColumn("dbo.AdminRoles", "RoleId");
            AddColumn("dbo.AdminRoles", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.UserAdminRoles", "AdminRoleId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.AdminRoles", "Id");
            CreateIndex("dbo.UserAdminRoles", "AdminRoleId");
            AddForeignKey("dbo.UserAdminRoles", "AdminRoleId", "dbo.AdminRoles", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            AddColumn("dbo.AdminRoles", "RoleId", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.UserAdminRoles", "AdminRoleId", "dbo.AdminRoles");
            DropIndex("dbo.UserAdminRoles", new[] { "AdminRoleId" });
            DropPrimaryKey("dbo.AdminRoles");
            AlterColumn("dbo.UserAdminRoles", "AdminRoleId", c => c.Int());
            DropColumn("dbo.AdminRoles", "Id");
            AddPrimaryKey("dbo.AdminRoles", "RoleId");
            RenameColumn(table: "dbo.UserAdminRoles", name: "AdminRoleId", newName: "AdminRole_RoleId");
            AddColumn("dbo.UserAdminRoles", "AdminRoleId", c => c.Int(nullable: false));
            CreateIndex("dbo.UserAdminRoles", "AdminRole_RoleId");
            AddForeignKey("dbo.UserAdminRoles", "AdminRole_RoleId", "dbo.AdminRoles", "RoleId");
        }
    }
}
