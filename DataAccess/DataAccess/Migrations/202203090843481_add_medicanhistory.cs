namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_medicanhistory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IPDMedicationHistories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VisitId = c.Guid(),
                        RoomId = c.String(),
                        PharmacistConfirm = c.String(),
                        PharmacistConfirmAt = c.DateTime(),
                        DoctorConfirm = c.String(),
                        DoctorConfirmAt = c.DateTime(),
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
            DropTable("dbo.IPDMedicationHistories");
        }
    }
}
