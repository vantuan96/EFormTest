namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatecovideasssitotavke : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IPDTakeCareOfPatientsWithCovid19Assessment", "AssessmentAt", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.IPDTakeCareOfPatientsWithCovid19Assessment", "AssessmentAt");
        }
    }
}
