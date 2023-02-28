namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatespecialtyrenameheadcode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Specialties", "LocationCode", c => c.String());
            DropColumn("dbo.Specialties", "HeadCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Specialties", "HeadCode", c => c.String());
            DropColumn("dbo.Specialties", "LocationCode");
        }
    }
}
