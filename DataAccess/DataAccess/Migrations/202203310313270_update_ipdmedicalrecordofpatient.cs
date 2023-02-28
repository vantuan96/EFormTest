namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_ipdmedicalrecordofpatient : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.IPDMedicalRecordOfPatients");
            AddColumn("dbo.IPDMedicalRecordOfPatients", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.IPDMedicalRecordOfPatients", "FormCode", c => c.String(maxLength: 50));
            AddPrimaryKey("dbo.IPDMedicalRecordOfPatients", "Id");
            DropColumn("dbo.IPDMedicalRecordOfPatients", "IPDMedicalRecordId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.IPDMedicalRecordOfPatients", "IPDMedicalRecordId", c => c.Guid(nullable: false));
            DropPrimaryKey("dbo.IPDMedicalRecordOfPatients");
            AlterColumn("dbo.IPDMedicalRecordOfPatients", "FormCode", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.IPDMedicalRecordOfPatients", "Id");
            AddPrimaryKey("dbo.IPDMedicalRecordOfPatients", new[] { "IPDMedicalRecordId", "FormCode" });
        }
    }
}
