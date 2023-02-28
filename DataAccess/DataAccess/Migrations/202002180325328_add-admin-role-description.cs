namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addadminroledescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdminRoles", "RoleDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AdminRoles", "RoleDescription");
        }
    }
}
