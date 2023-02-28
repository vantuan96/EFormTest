namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetableIPDConfirmDischarge : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IPDConfirmDischarges", "ReasonDischarge", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.IPDConfirmDischarges", "ReasonDischarge");
        }
    }
}
