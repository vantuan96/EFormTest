namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateorderinfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Drug", c => c.String());
            AddColumn("dbo.Orders", "Dosage", c => c.String());
            AddColumn("dbo.Orders", "Route", c => c.String());
            AddColumn("dbo.Orders", "UsedAt", c => c.DateTime());
            AddColumn("dbo.Orders", "MedicalStaffName", c => c.String());
            AddColumn("dbo.Orders", "DoctorConfirm", c => c.String());
            AddColumn("dbo.Orders", "IsConfirm", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "Status", c => c.String());
            DropColumn("dbo.Orders", "Dir");
            DropColumn("dbo.Orders", "Dr");
            DropColumn("dbo.Orders", "Time");
            DropColumn("dbo.Orders", "RN");
            DropColumn("dbo.Orders", "LabTestImaging");
            DropColumn("dbo.Orders", "POCT");
            DropColumn("dbo.Orders", "TimeNotified");
            DropColumn("dbo.Orders", "DrArrivalTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "DrArrivalTime", c => c.Time(precision: 7));
            AddColumn("dbo.Orders", "TimeNotified", c => c.Time(precision: 7));
            AddColumn("dbo.Orders", "POCT", c => c.String());
            AddColumn("dbo.Orders", "LabTestImaging", c => c.String());
            AddColumn("dbo.Orders", "RN", c => c.String());
            AddColumn("dbo.Orders", "Time", c => c.Time(precision: 7));
            AddColumn("dbo.Orders", "Dr", c => c.String());
            AddColumn("dbo.Orders", "Dir", c => c.String());
            DropColumn("dbo.Orders", "Status");
            DropColumn("dbo.Orders", "IsConfirm");
            DropColumn("dbo.Orders", "DoctorConfirm");
            DropColumn("dbo.Orders", "MedicalStaffName");
            DropColumn("dbo.Orders", "UsedAt");
            DropColumn("dbo.Orders", "Route");
            DropColumn("dbo.Orders", "Dosage");
            DropColumn("dbo.Orders", "Drug");
        }
    }
}
