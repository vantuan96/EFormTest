namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_field_inventionOther_Glamorgan : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IPDGlamorgans", "InterventionOther", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.IPDGlamorgans", "InterventionOther");
        }
    }
}
