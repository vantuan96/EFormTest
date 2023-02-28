namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addsumaryof15daytreatment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IPDSummaryOf15DayTreatment",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VisitId = c.Guid(),
                        RoomId = c.String(),
                        HeadOfDepartmentConfirm = c.String(),
                        HeadOfDepartmentConfirmAt = c.DateTime(),
                        PhysicianConfirm = c.String(),
                        PhysicianConfirmAt = c.DateTime(),
                        Order = c.Int(nullable: false, identity: true),
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
            DropTable("dbo.IPDSummaryOf15DayTreatment");
        }
    }
}
