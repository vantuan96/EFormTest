namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inittblEOCOutpatientExaminationNote : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EOCOutpatientExaminationNotes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ExaminationTime = c.DateTime(),
                        Version = c.Int(nullable: false),
                        VisitId = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EOCs", t => t.VisitId)
                .Index(t => t.VisitId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EOCOutpatientExaminationNotes", "VisitId", "dbo.EOCs");
            DropIndex("dbo.EOCOutpatientExaminationNotes", new[] { "VisitId" });
            DropTable("dbo.EOCOutpatientExaminationNotes");
        }
    }
}
