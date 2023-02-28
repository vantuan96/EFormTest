namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfielddatatypeipdiaspecialrequest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IPDInitialAssessmentSpecialRequests", "DataType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.IPDInitialAssessmentSpecialRequests", "DataType");
        }
    }
}
