namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcolspecimenstatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChargeItems", "SpecimenStatus", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ChargeItems", "SpecimenStatus");
        }
    }
}
