namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_status : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "EDStatusId", c => c.Guid());
            CreateIndex("dbo.Customers", "EDStatusId");
            AddForeignKey("dbo.Customers", "EDStatusId", "dbo.EDStatus", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "EDStatusId", "dbo.EDStatus");
            DropIndex("dbo.Customers", new[] { "EDStatusId" });
            DropColumn("dbo.Customers", "EDStatusId");
        }
    }
}
