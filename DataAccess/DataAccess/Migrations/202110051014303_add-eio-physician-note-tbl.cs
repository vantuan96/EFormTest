namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addeiophysiciannotetbl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EIOPhysicianNotes",
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
                        NoteTime = c.DateTime(),
                        Examination = c.String(),
                        Treatment = c.String(),
                        VisitId = c.Guid(),
                        VisitTypeGroupCode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EIOPhysicianNotes");
        }
    }
}
