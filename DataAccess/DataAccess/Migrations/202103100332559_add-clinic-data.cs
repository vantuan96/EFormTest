namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addclinicdata : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clinics", "Data", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clinics", "Data");
        }
    }
}
