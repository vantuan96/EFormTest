namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class udateuserad : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "FirstName", c => c.String());
            AddColumn("dbo.Users", "LastName", c => c.String());
            AddColumn("dbo.Users", "MiddleName", c => c.String());
            AddColumn("dbo.Users", "DisplayName", c => c.String());
            AddColumn("dbo.Users", "LoginNameWithDomain", c => c.String());
            AddColumn("dbo.Users", "Mobile", c => c.String());
            AddColumn("dbo.Users", "EmailAddress", c => c.String());
            AddColumn("dbo.Users", "Department", c => c.String());
            AddColumn("dbo.Users", "Title", c => c.String());
            AddColumn("dbo.Users", "Description", c => c.String());
            AddColumn("dbo.Users", "Company", c => c.String());
            AddColumn("dbo.Users", "ManagerName", c => c.String());
            AddColumn("dbo.Users", "ManagerId", c => c.String());
            DropColumn("dbo.Users", "Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Email", c => c.String());
            DropColumn("dbo.Users", "ManagerId");
            DropColumn("dbo.Users", "ManagerName");
            DropColumn("dbo.Users", "Company");
            DropColumn("dbo.Users", "Description");
            DropColumn("dbo.Users", "Title");
            DropColumn("dbo.Users", "Department");
            DropColumn("dbo.Users", "EmailAddress");
            DropColumn("dbo.Users", "Mobile");
            DropColumn("dbo.Users", "LoginNameWithDomain");
            DropColumn("dbo.Users", "DisplayName");
            DropColumn("dbo.Users", "MiddleName");
            DropColumn("dbo.Users", "LastName");
            DropColumn("dbo.Users", "FirstName");
        }
    }
}
