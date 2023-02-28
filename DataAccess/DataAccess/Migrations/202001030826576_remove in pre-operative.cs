namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeinpreoperative : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PreOperativeProcedureHandoverChecklists", "DateOfOperation");
            DropColumn("dbo.PreOperativeProcedureHandoverChecklists", "DateTimeTest");
            DropColumn("dbo.PreOperativeProcedureHandoverChecklists", "ScrubNurse");
            DropColumn("dbo.PreOperativeProcedureHandoverChecklists", "CirculatingNurse");
            DropColumn("dbo.PreOperativeProcedureHandoverChecklists", "Surgeon");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PreOperativeProcedureHandoverChecklists", "Surgeon", c => c.String());
            AddColumn("dbo.PreOperativeProcedureHandoverChecklists", "CirculatingNurse", c => c.String());
            AddColumn("dbo.PreOperativeProcedureHandoverChecklists", "ScrubNurse", c => c.String());
            AddColumn("dbo.PreOperativeProcedureHandoverChecklists", "DateTimeTest", c => c.DateTime());
            AddColumn("dbo.PreOperativeProcedureHandoverChecklists", "DateOfOperation", c => c.DateTime());
        }
    }
}
