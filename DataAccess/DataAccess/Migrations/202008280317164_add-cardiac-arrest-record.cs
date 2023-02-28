namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcardiacarrestrecord : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EIOCardiacArrestRecordDatas",
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
                        Type = c.String(),
                        Code = c.String(),
                        Value = c.String(),
                        FormId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EIOCardiacArrestRecords",
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
                        VisitId = c.Guid(),
                        VisitTypeGroupCode = c.String(),
                        Note = c.String(),
                        DoctorComfirm = c.DateTime(),
                        DoctorId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.DoctorId)
                .Index(t => t.DoctorId);
            
            CreateTable(
                "dbo.EIOCardiacArrestRecordTables",
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
                        Time = c.DateTime(),
                        EIOCardiacArrestRecordId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EIOCardiacArrestRecords", t => t.EIOCardiacArrestRecordId)
                .Index(t => t.EIOCardiacArrestRecordId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EIOCardiacArrestRecordTables", "EIOCardiacArrestRecordId", "dbo.EIOCardiacArrestRecords");
            DropForeignKey("dbo.EIOCardiacArrestRecords", "DoctorId", "dbo.Users");
            DropIndex("dbo.EIOCardiacArrestRecordTables", new[] { "EIOCardiacArrestRecordId" });
            DropIndex("dbo.EIOCardiacArrestRecords", new[] { "DoctorId" });
            DropTable("dbo.EIOCardiacArrestRecordTables");
            DropTable("dbo.EIOCardiacArrestRecords");
            DropTable("dbo.EIOCardiacArrestRecordDatas");
        }
    }
}
