namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetranferendoscopy : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EOCTransfers", "CustomerId", "dbo.Customers");
            DropIndex("dbo.EOCTransfers", new[] { "CustomerId" });
            DropColumn("dbo.EOCTransfers", "CustomerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EOCTransfers", "CustomerId", c => c.Guid());
            CreateIndex("dbo.EOCTransfers", "CustomerId");
            AddForeignKey("dbo.EOCTransfers", "CustomerId", "dbo.Customers", "Id");
        }
    }
}
