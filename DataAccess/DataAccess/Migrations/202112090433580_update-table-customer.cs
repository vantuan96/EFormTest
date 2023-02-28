namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetablecustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "PIDOH", c => c.String(maxLength: 100, unicode: false));
            CreateIndex("dbo.Customers", "PIDOH");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Customers", new[] { "PIDOH" });
            DropColumn("dbo.Customers", "PIDOH");
        }
    }
}
