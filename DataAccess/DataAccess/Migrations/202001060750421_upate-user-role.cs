namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upateuserrole : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UserSites", newName: "UserRoles");
            RenameColumn(table: "dbo.UserRoles", name: "SiteId", newName: "Site_Id");
            RenameColumn(table: "dbo.Users", name: "RoleId", newName: "Role_Id");
            RenameIndex(table: "dbo.Users", name: "IX_RoleId", newName: "IX_Role_Id");
            RenameIndex(table: "dbo.UserRoles", name: "IX_SiteId", newName: "IX_Site_Id");
            AddColumn("dbo.UserRoles", "RoleId", c => c.Guid());
            CreateIndex("dbo.UserRoles", "RoleId");
            AddForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropColumn("dbo.UserRoles", "RoleId");
            RenameIndex(table: "dbo.UserRoles", name: "IX_Site_Id", newName: "IX_SiteId");
            RenameIndex(table: "dbo.Users", name: "IX_Role_Id", newName: "IX_RoleId");
            RenameColumn(table: "dbo.Users", name: "Role_Id", newName: "RoleId");
            RenameColumn(table: "dbo.UserRoles", name: "Site_Id", newName: "SiteId");
            RenameTable(name: "dbo.UserRoles", newName: "UserSites");
        }
    }
}
