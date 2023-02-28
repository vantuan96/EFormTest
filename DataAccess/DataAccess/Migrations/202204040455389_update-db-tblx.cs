namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedbtblx : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IPDMedicalRecordOfPatients", "FormId", c => c.Guid());
            DropColumn("dbo.IPDMedicalRecordOfPatients", "IPDMedicalRecordPart1Id");
            DropColumn("dbo.IPDMedicalRecordOfPatients", "IPDMedicalRecordPart2Id");
            DropColumn("dbo.IPDMedicalRecordOfPatients", "IPDMedicalRecordPart3Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.IPDMedicalRecordOfPatients", "IPDMedicalRecordPart3Id", c => c.Guid());
            AddColumn("dbo.IPDMedicalRecordOfPatients", "IPDMedicalRecordPart2Id", c => c.Guid());
            AddColumn("dbo.IPDMedicalRecordOfPatients", "IPDMedicalRecordPart1Id", c => c.Guid());
            DropColumn("dbo.IPDMedicalRecordOfPatients", "FormId");
        }
    }
}
