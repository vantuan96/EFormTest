namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addopdpatientprogressnote : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OPDPatientProgressNoteDatas",
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
                        OPDPatientProgressNoteId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OPDPatientProgressNotes", t => t.OPDPatientProgressNoteId)
                .Index(t => t.OPDPatientProgressNoteId);
            
            CreateTable(
                "dbo.OPDPatientProgressNotes",
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
            
            AddColumn("dbo.OPDs", "OPDPatientProgressNoteId", c => c.Guid());
            CreateIndex("dbo.OPDs", "OPDPatientProgressNoteId");
            AddForeignKey("dbo.OPDs", "OPDPatientProgressNoteId", "dbo.OPDPatientProgressNotes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OPDs", "OPDPatientProgressNoteId", "dbo.OPDPatientProgressNotes");
            DropForeignKey("dbo.OPDPatientProgressNoteDatas", "OPDPatientProgressNoteId", "dbo.OPDPatientProgressNotes");
            DropIndex("dbo.OPDs", new[] { "OPDPatientProgressNoteId" });
            DropIndex("dbo.OPDPatientProgressNoteDatas", new[] { "OPDPatientProgressNoteId" });
            DropColumn("dbo.OPDs", "OPDPatientProgressNoteId");
            DropTable("dbo.OPDPatientProgressNotes");
            DropTable("dbo.OPDPatientProgressNoteDatas");
        }
    }
}
