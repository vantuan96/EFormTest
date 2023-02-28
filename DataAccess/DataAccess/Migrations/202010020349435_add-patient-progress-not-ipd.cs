namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addpatientprogressnotipd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IPDPatientProgressNoteDatas",
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
                        DateTime = c.DateTime(),
                        Note = c.String(),
                        Interventions = c.String(),
                        NoteAt = c.DateTime(),
                        IPDPatientProgressNoteId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IPDPatientProgressNotes", t => t.IPDPatientProgressNoteId)
                .Index(t => t.IPDPatientProgressNoteId);
            
            CreateTable(
                "dbo.IPDPatientProgressNotes",
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
                        NoteAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.IPDs", "IPDPatientProgressNoteId", c => c.Guid());
            CreateIndex("dbo.IPDs", "IPDPatientProgressNoteId");
            AddForeignKey("dbo.IPDs", "IPDPatientProgressNoteId", "dbo.IPDPatientProgressNotes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IPDs", "IPDPatientProgressNoteId", "dbo.IPDPatientProgressNotes");
            DropForeignKey("dbo.IPDPatientProgressNoteDatas", "IPDPatientProgressNoteId", "dbo.IPDPatientProgressNotes");
            DropIndex("dbo.IPDs", new[] { "IPDPatientProgressNoteId" });
            DropIndex("dbo.IPDPatientProgressNoteDatas", new[] { "IPDPatientProgressNoteId" });
            DropColumn("dbo.IPDs", "IPDPatientProgressNoteId");
            DropTable("dbo.IPDPatientProgressNotes");
            DropTable("dbo.IPDPatientProgressNoteDatas");
        }
    }
}
