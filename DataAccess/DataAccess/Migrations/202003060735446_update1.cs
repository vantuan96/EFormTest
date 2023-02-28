namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OPDs", "ClinicId", c => c.Guid());
            CreateIndex("dbo.OPDs", "ClinicId");
            AddForeignKey("dbo.OPDs", "ClinicId", "dbo.Clinics", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OPDs", "ClinicId", "dbo.Clinics");
            DropIndex("dbo.OPDs", new[] { "ClinicId" });
            DropColumn("dbo.OPDs", "ClinicId");
        }
    }
}
