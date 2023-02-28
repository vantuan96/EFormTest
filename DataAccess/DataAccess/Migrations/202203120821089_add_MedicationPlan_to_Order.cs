namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_MedicationPlan_to_Order : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "MedicationPlan", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "MedicationPlan");
        }
    }
}
