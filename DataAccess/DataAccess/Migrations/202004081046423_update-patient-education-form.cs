namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatepatienteducationform : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PatientAndFamilyEducationDatas",
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
                        EnValue = c.String(),
                        PatientAndFamilyEducationId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PatientAndFamilyEducations", t => t.PatientAndFamilyEducationId)
                .Index(t => t.PatientAndFamilyEducationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PatientAndFamilyEducationDatas", "PatientAndFamilyEducationId", "dbo.PatientAndFamilyEducations");
            DropIndex("dbo.PatientAndFamilyEducationDatas", new[] { "PatientAndFamilyEducationId" });
            DropTable("dbo.PatientAndFamilyEducationDatas");
        }
    }
}
