namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_tbl_IPDInitialAssessmentForNewborns : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IPDInitialAssessmentForNewborns",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VisitId = c.Guid(),
                        RoomId = c.String(),
                        MedicalStaff1ConfirmId = c.Guid(),
                        MedicalStaff1ConfirmAt = c.DateTime(),
                        MedicalStaff2ConfirmId = c.Guid(),
                        MedicalStaff2ConfirmAt = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.IPDInitialAssessmentForNewborns");
        }
    }
}
