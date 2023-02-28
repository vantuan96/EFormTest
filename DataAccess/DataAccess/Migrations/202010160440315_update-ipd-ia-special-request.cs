namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateipdiaspecialrequest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IPDInitialAssessmentSpecialRequests", "ViValue", c => c.String());
            AddColumn("dbo.IPDInitialAssessmentSpecialRequests", "EnValue", c => c.String());
            AddColumn("dbo.IPDInitialAssessmentSpecialRequests", "Order", c => c.Int(nullable: false));
            DropColumn("dbo.IPDInitialAssessmentSpecialRequests", "Value");
        }
        
        public override void Down()
        {
            AddColumn("dbo.IPDInitialAssessmentSpecialRequests", "Value", c => c.String());
            DropColumn("dbo.IPDInitialAssessmentSpecialRequests", "Order");
            DropColumn("dbo.IPDInitialAssessmentSpecialRequests", "EnValue");
            DropColumn("dbo.IPDInitialAssessmentSpecialRequests", "ViValue");
        }
    }
}
