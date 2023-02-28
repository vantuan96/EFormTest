namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_column_table_ipdsetmedicalrecord : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IPDSetupMedicalRecords", "FormType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.IPDSetupMedicalRecords", "FormType");
        }
    }
}
