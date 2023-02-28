namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create_Prescription : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PrescriptionNoteModels",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PrescriptionId = c.Guid(nullable: false),
                        PrescriptionType = c.String(),
                        Note = c.String(),
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
            DropTable("dbo.PrescriptionNoteModels");
        }
    }
}
