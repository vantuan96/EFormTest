namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatehumanresourceassessment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HumanResourceAssessmentStaffs", "Order", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HumanResourceAssessmentStaffs", "Order");
        }
    }
}
