namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtimecolsx : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IPDDischargePreparationChecklists", "Time", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.IPDDischargePreparationChecklists", "Time");
        }
    }
}
