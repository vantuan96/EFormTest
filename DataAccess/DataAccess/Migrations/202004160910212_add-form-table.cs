namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addformtable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Forms",
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
                        Name = c.String(),
                        Code = c.String(),
                        VisitTypeGroupCode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.UnlockFormToUpdates", "FormCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UnlockFormToUpdates", "FormCode");
            DropTable("dbo.Forms");
        }
    }
}
