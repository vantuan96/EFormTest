namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addjobincutomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Job", c => c.String());
            AddColumn("dbo.Customers", "Fork", c => c.String());
            AddColumn("dbo.Customers", "Inssurance", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "Inssurance");
            DropColumn("dbo.Customers", "Fork");
            DropColumn("dbo.Customers", "Job");
        }
    }
}
