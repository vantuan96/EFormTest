namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_table_ipdsetupmedicalrecord : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IPDSetupMedicalRecords", "Type", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.IPDSetupMedicalRecords", "Type");
        }
    }
}
