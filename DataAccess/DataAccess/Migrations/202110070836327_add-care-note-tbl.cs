namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcarenotetbl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EIOCareNotes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        NoteTime = c.DateTime(),
                        ProgressNote = c.String(),
                        CareNote = c.String(),
                        VisitId = c.Guid(),
                        VisitTypeGroupCode = c.String(),
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
            DropTable("dbo.EIOCareNotes");
        }
    }
}
