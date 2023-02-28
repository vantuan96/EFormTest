namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Add_Column_IPDMedicalRecordOfPatient : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IPDMedicalRecordOfPatients", "IPDMedicalRecordPart1Id", c => c.Guid());
            AddColumn("dbo.IPDMedicalRecordOfPatients", "IPDMedicalRecordPart2Id", c => c.Guid());
            AddColumn("dbo.IPDMedicalRecordOfPatients", "IPDMedicalRecordPart3Id", c => c.Guid());
            AlterColumn("dbo.IPDMedicalRecordOfPatients", "FormCode", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.IPDMedicalRecordOfPatients", "FormCode", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.IPDMedicalRecordOfPatients", "IPDMedicalRecordPart3Id");
            DropColumn("dbo.IPDMedicalRecordOfPatients", "IPDMedicalRecordPart2Id");
            DropColumn("dbo.IPDMedicalRecordOfPatients", "IPDMedicalRecordPart1Id");
            AddPrimaryKey("dbo.IPDMedicalRecordOfPatients", new[] { "IPDMedicalRecordId", "FormCode" });
        }
    }
}
