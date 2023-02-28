namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmedicalrecorddata : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IPDMedicalRecordDatas",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                        Code = c.String(),
                        Value = c.String(),
                        IPDMedicalRecordId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IPDMedicalRecords", t => t.IPDMedicalRecordId)
                .Index(t => t.IPDMedicalRecordId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IPDMedicalRecordDatas", "IPDMedicalRecordId", "dbo.IPDMedicalRecords");
            DropIndex("dbo.IPDMedicalRecordDatas", new[] { "IPDMedicalRecordId" });
            DropTable("dbo.IPDMedicalRecordDatas");
        }
    }
}
