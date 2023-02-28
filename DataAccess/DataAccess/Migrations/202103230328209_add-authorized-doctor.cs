namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addauthorizeddoctor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OPDs", "AuthorizedDoctorId", c => c.Guid());
            CreateIndex("dbo.OPDs", "AuthorizedDoctorId");
            AddForeignKey("dbo.OPDs", "AuthorizedDoctorId", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OPDs", "AuthorizedDoctorId", "dbo.Users");
            DropIndex("dbo.OPDs", new[] { "AuthorizedDoctorId" });
            DropColumn("dbo.OPDs", "AuthorizedDoctorId");
        }
    }
}
