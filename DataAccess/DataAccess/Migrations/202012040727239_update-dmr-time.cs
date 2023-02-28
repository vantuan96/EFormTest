namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedmrtime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IPDDischargeMedicalReports", "PhysicianInChargeTime", c => c.DateTime());
            AddColumn("dbo.IPDDischargeMedicalReports", "DeptHeadTime", c => c.DateTime());
            AddColumn("dbo.IPDDischargeMedicalReports", "DirectorTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.IPDDischargeMedicalReports", "DirectorTime");
            DropColumn("dbo.IPDDischargeMedicalReports", "DeptHeadTime");
            DropColumn("dbo.IPDDischargeMedicalReports", "PhysicianInChargeTime");
        }
    }
}
