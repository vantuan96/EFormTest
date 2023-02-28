namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class doctocoirm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "DoctorId", c => c.Guid());
            CreateIndex("dbo.Orders", "DoctorId");
            AddForeignKey("dbo.Orders", "DoctorId", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "DoctorId", "dbo.Users");
            DropIndex("dbo.Orders", new[] { "DoctorId" });
            DropColumn("dbo.Orders", "DoctorId");
        }
    }
}
