namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_tbl_IPDInitialAssessmentForPediatricInpatient : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.IPDInitialAssessmentForPediatricInpatients",
            //    c => new
            //        {
            //            Id = c.Guid(nullable: false),
            //            VisitId = c.Guid(),
            //            RoomId = c.String(),
            //            IsDeleted = c.Boolean(nullable: false),
            //            DeletedBy = c.String(),
            //            DeletedAt = c.DateTime(),
            //            CreatedBy = c.String(),
            //            CreatedAt = c.DateTime(),
            //            UpdatedBy = c.String(),
            //            UpdatedAt = c.DateTime(),
            //        })
            //    .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {          
            DropTable("dbo.IPDInitialAssessmentForPediatricInpatients");
        }
    }
}
