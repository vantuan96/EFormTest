namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_tableVitalSignForAdult : DbMigration
    {
        public override void Up()
        {
            // RenameColumn(table: "dbo.IPD_VITALSIGN_ADULT", name: "RESPIRATORY_SUPORT", newName: "RESPIRATORY_SUPPORT");
        }
        
        public override void Down()
        {
            // RenameColumn(table: "dbo.IPD_VITALSIGN_ADULT", name: "RESPIRATORY_SUPPORT", newName: "RESPIRATORY_SUPORT");
        }
    }
}
