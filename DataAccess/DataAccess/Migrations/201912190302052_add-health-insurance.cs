namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addhealthinsurance : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EDs", "DischargeDate", c => c.DateTime());
            AddColumn("dbo.EDs", "HealthInsuranceNumber", c => c.String());
            AddColumn("dbo.EDs", "StartHealthInsuranceDate", c => c.DateTime());
            AddColumn("dbo.EDs", "ExpireHealthInsuranceDate", c => c.DateTime());
            DropColumn("dbo.Customers", "Inssurance");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "Inssurance", c => c.String());
            DropColumn("dbo.EDs", "ExpireHealthInsuranceDate");
            DropColumn("dbo.EDs", "StartHealthInsuranceDate");
            DropColumn("dbo.EDs", "HealthInsuranceNumber");
            DropColumn("dbo.EDs", "DischargeDate");
        }
    }
}
