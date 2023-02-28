namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_ed_status : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EDs", "CustomerStatusId", c => c.Guid());
            CreateIndex("dbo.EDs", "CustomerStatusId");
            AddForeignKey("dbo.EDs", "CustomerStatusId", "dbo.CustomerStatus", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EDs", "CustomerStatusId", "dbo.CustomerStatus");
            DropIndex("dbo.EDs", new[] { "CustomerStatusId" });
            DropColumn("dbo.EDs", "CustomerStatusId");
        }
    }
}
