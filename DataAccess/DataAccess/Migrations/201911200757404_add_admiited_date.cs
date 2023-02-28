namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_admiited_date : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "AdmittedDate", c => c.DateTime());
            AddColumn("dbo.EDs", "AdmittedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EDs", "AdmittedDate");
            DropColumn("dbo.Customers", "AdmittedDate");
        }
    }
}
