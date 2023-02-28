namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addipdplanofcare : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IPDPlanOfCares",
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
                        IPDId = c.Guid(),
                        Time = c.DateTime(),
                        Diagnosis = c.String(),
                        FollowUpCarePlan = c.String(),
                        ParaClinicalTestsPlan = c.String(),
                        SpecialRequests = c.String(),
                        EducationPlan = c.String(),
                        ExpectedNumber = c.String(),
                        Prognosis = c.String(),
                        ExpectedOutcome = c.String(),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IPDs", t => t.IPDId)
                .Index(t => t.IPDId);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IPDPlanOfCares", "IPDId", "dbo.IPDs");
            DropIndex("dbo.IPDPlanOfCares", new[] { "IPDId" });
            DropTable("dbo.IPDPlanOfCares");
        }
    }
}
