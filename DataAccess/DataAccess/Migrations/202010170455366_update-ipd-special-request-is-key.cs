namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateipdspecialrequestiskey : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IPDInitialAssessmentSpecialRequests", "IsKey", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.IPDInitialAssessmentSpecialRequests", "IsKey");
        }
    }
}
