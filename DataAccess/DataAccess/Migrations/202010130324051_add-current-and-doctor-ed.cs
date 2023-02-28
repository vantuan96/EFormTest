namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcurrentanddoctored : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EDs", "Reason", c => c.String());
            AddColumn("dbo.EDs", "CurrentDoctorId", c => c.Guid());
            AddColumn("dbo.EDs", "CurrentNurseId", c => c.Guid());
            CreateIndex("dbo.EDs", "CurrentDoctorId");
            CreateIndex("dbo.EDs", "CurrentNurseId");
            AddForeignKey("dbo.EDs", "CurrentDoctorId", "dbo.Users", "Id");
            AddForeignKey("dbo.EDs", "CurrentNurseId", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EDs", "CurrentNurseId", "dbo.Users");
            DropForeignKey("dbo.EDs", "CurrentDoctorId", "dbo.Users");
            DropIndex("dbo.EDs", new[] { "CurrentNurseId" });
            DropIndex("dbo.EDs", new[] { "CurrentDoctorId" });
            DropColumn("dbo.EDs", "CurrentNurseId");
            DropColumn("dbo.EDs", "CurrentDoctorId");
            DropColumn("dbo.EDs", "Reason");
        }
    }
}
