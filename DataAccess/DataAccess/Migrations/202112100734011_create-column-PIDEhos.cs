namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createcolumnPIDEhos : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Customers", new[] { "PIDOH" });
            AddColumn("dbo.Customers", "PIDEhos", c => c.String(maxLength: 100, unicode: false));
            CreateIndex("dbo.Customers", "PIDEhos");
            DropColumn("dbo.Customers", "PIDOH");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "PIDOH", c => c.String(maxLength: 100, unicode: false));
            DropIndex("dbo.Customers", new[] { "PIDEhos" });
            DropColumn("dbo.Customers", "PIDEhos");
            CreateIndex("dbo.Customers", "PIDOH");
        }
    }
}
