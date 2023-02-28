namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtableformdatas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FormDatas",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Code = c.String(),
                        Value = c.String(),
                        FormCode = c.String(),
                        FormId = c.Guid(),
                        VisitId = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FormDatas");
        }
    }
}
