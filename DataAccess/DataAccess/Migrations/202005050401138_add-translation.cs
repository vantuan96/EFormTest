namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtranslation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TranslationDatas",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTime(),
                        DeletedBy = c.String(),
                        TranslationId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Translations", t => t.TranslationId)
                .Index(t => t.TranslationId);
            
            CreateTable(
                "dbo.Translations",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTime(),
                        DeletedBy = c.String(),
                        ViName = c.String(),
                        EnName = c.String(),
                        TranslatedBy = c.String(),
                        Language = c.String(),
                        Note = c.String(),
                        Status = c.String(),
                        CustomerId = c.Guid(),
                        VisitId = c.Guid(),
                        VisitTypeGroupCode = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TranslationDatas", "TranslationId", "dbo.Translations");
            DropForeignKey("dbo.Translations", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Translations", new[] { "CustomerId" });
            DropIndex("dbo.TranslationDatas", new[] { "TranslationId" });
            DropTable("dbo.Translations");
            DropTable("dbo.TranslationDatas");
        }
    }
}
