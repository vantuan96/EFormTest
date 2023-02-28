namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updaterolev2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "RoleId", c => c.Guid());
            AddColumn("dbo.Actions", "Name", c => c.String());
            AddColumn("dbo.Actions", "Code", c => c.String());
            CreateIndex("dbo.Users", "RoleId");
            AddForeignKey("dbo.Users", "RoleId", "dbo.Roles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropColumn("dbo.Actions", "Code");
            DropColumn("dbo.Actions", "Name");
            DropColumn("dbo.Users", "RoleId");
        }
    }
}
