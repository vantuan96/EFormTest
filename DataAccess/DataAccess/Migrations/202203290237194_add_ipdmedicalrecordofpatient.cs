namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_ipdmedicalrecordofpatient : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IPDMedicalRecordOfPatients",
                c => new
                    {
                        IPDMedicalRecordId = c.Guid(nullable: false),
                        FormCode = c.String(nullable: false, maxLength: 50),
                        VisitId = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.IPDMedicalRecordId, t.FormCode });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.IPDMedicalRecordOfPatients");
        }
    }
}
