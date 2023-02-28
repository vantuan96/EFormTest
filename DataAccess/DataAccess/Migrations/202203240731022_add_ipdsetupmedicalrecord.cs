namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _202203230350125_add_ipdsetupmedicalrecord : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IPDSetupMedicalRecords",
                c => new
                    {
                        SpecialityId = c.Guid(nullable: false),
                        Formcode = c.String(nullable: false, maxLength: 50),
                        ViName = c.String(),
                        EnName = c.String(),
                        IsDeploy = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.SpecialityId, t.Formcode });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.IPDSetupMedicalRecords");
        }
    }
}
