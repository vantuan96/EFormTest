namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateChargetbll : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppConfigs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Key = c.String(),
                        Label = c.String(),
                        Value = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Charges", "Room", c => c.String());
            AddColumn("dbo.Charges", "Bed", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Charges", "Bed");
            DropColumn("dbo.Charges", "Room");
            DropTable("dbo.AppConfigs");
        }
    }
}
