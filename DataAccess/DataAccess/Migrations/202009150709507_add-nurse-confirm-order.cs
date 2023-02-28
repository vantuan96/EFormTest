namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnurseconfirmorder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "DoctorTime", c => c.DateTime());
            AddColumn("dbo.Orders", "NurseTime", c => c.DateTime());
            AddColumn("dbo.Orders", "NurseId", c => c.Guid());
            CreateIndex("dbo.Orders", "NurseId");
            AddForeignKey("dbo.Orders", "NurseId", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "NurseId", "dbo.Users");
            DropIndex("dbo.Orders", new[] { "NurseId" });
            DropColumn("dbo.Orders", "NurseId");
            DropColumn("dbo.Orders", "NurseTime");
            DropColumn("dbo.Orders", "DoctorTime");
        }
    }
}
