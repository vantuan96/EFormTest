namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addstandingordermasterdata : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EDs", "PrimaryDoctorId", c => c.Guid());
            AddColumn("dbo.OPDs", "IsBooked", c => c.Boolean(nullable: false));
            AddColumn("dbo.OPDs", "BookingTime", c => c.DateTime());
            AddColumn("dbo.OPDs", "PrimaryDoctorId", c => c.Guid());
            AddColumn("dbo.Orders", "OPDId", c => c.Guid());
            CreateIndex("dbo.EDs", "PrimaryDoctorId");
            CreateIndex("dbo.OPDs", "PrimaryDoctorId");
            CreateIndex("dbo.Orders", "OPDId");
            AddForeignKey("dbo.Orders", "OPDId", "dbo.OPDs", "Id");
            AddForeignKey("dbo.OPDs", "PrimaryDoctorId", "dbo.Users", "Id");
            AddForeignKey("dbo.EDs", "PrimaryDoctorId", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EDs", "PrimaryDoctorId", "dbo.Users");
            DropForeignKey("dbo.OPDs", "PrimaryDoctorId", "dbo.Users");
            DropForeignKey("dbo.Orders", "OPDId", "dbo.OPDs");
            DropIndex("dbo.Orders", new[] { "OPDId" });
            DropIndex("dbo.OPDs", new[] { "PrimaryDoctorId" });
            DropIndex("dbo.EDs", new[] { "PrimaryDoctorId" });
            DropColumn("dbo.Orders", "OPDId");
            DropColumn("dbo.OPDs", "PrimaryDoctorId");
            DropColumn("dbo.OPDs", "BookingTime");
            DropColumn("dbo.OPDs", "IsBooked");
            DropColumn("dbo.EDs", "PrimaryDoctorId");
        }
    }
}
