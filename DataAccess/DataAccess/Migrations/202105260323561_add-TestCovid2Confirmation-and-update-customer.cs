namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTestCovid2Confirmationandupdatecustomer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EIOTestCovid2Confirmation",
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
                        Time = c.DateTime(),
                        MethodTest = c.String(),
                        Result = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Customers", "AddressEn", c => c.String());
            AddColumn("dbo.Customers", "NationalityEn", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "NationalityEn");
            DropColumn("dbo.Customers", "AddressEn");
            DropTable("dbo.EIOTestCovid2Confirmation");
        }
    }
}
