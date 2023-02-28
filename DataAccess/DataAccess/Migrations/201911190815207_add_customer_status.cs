namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_customer_status : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerStatus",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                        EnName = c.String(),
                        ViName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Customers", "MetDoctor", c => c.Boolean(nullable: false));
            AddColumn("dbo.Customers", "ATSScale", c => c.String());
            AddColumn("dbo.Customers", "CustomerStatusId", c => c.Guid());
            CreateIndex("dbo.Customers", "CustomerStatusId");
            AddForeignKey("dbo.Customers", "CustomerStatusId", "dbo.CustomerStatus", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "CustomerStatusId", "dbo.CustomerStatus");
            DropIndex("dbo.Customers", new[] { "CustomerStatusId" });
            DropColumn("dbo.Customers", "CustomerStatusId");
            DropColumn("dbo.Customers", "ATSScale");
            DropColumn("dbo.Customers", "MetDoctor");
            DropTable("dbo.CustomerStatus");
        }
    }
}
