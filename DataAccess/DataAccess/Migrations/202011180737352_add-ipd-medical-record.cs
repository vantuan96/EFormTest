namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addipdmedicalrecord : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IPDMedicalRecords",
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
                        IPDMedicalRecordPart1Id = c.Guid(),
                        IPDMedicalRecordPart2Id = c.Guid(),
                        IPDMedicalRecordPart3Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IPDMedicalRecordPart1", t => t.IPDMedicalRecordPart1Id)
                .ForeignKey("dbo.IPDMedicalRecordPart2", t => t.IPDMedicalRecordPart2Id)
                .ForeignKey("dbo.IPDMedicalRecordPart3", t => t.IPDMedicalRecordPart3Id)
                .Index(t => t.IPDMedicalRecordPart1Id)
                .Index(t => t.IPDMedicalRecordPart2Id)
                .Index(t => t.IPDMedicalRecordPart3Id);
            
            CreateTable(
                "dbo.IPDMedicalRecordPart1",
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
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IPDMedicalRecordPart1Data",
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
                        Code = c.String(),
                        Value = c.String(),
                        IPDMedicalRecordPart1Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IPDMedicalRecordPart1", t => t.IPDMedicalRecordPart1Id)
                .Index(t => t.IPDMedicalRecordPart1Id);
            
            CreateTable(
                "dbo.IPDMedicalRecordPart2",
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
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IPDMedicalRecordPart2Data",
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
                        Code = c.String(),
                        Value = c.String(),
                        IPDMedicalRecordPart2Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IPDMedicalRecordPart2", t => t.IPDMedicalRecordPart2Id)
                .Index(t => t.IPDMedicalRecordPart2Id);
            
            CreateTable(
                "dbo.IPDMedicalRecordPart3",
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
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IPDMedicalRecordPart3Data",
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
                        Code = c.String(),
                        Value = c.String(),
                        IPDMedicalRecordPart3Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IPDMedicalRecordPart3", t => t.IPDMedicalRecordPart3Id)
                .Index(t => t.IPDMedicalRecordPart3Id);
            
            AddColumn("dbo.IPDs", "IPDMedicalRecordId", c => c.Guid());
            CreateIndex("dbo.IPDs", "IPDMedicalRecordId");
            AddForeignKey("dbo.IPDs", "IPDMedicalRecordId", "dbo.IPDMedicalRecords", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IPDs", "IPDMedicalRecordId", "dbo.IPDMedicalRecords");
            DropForeignKey("dbo.IPDMedicalRecords", "IPDMedicalRecordPart3Id", "dbo.IPDMedicalRecordPart3");
            DropForeignKey("dbo.IPDMedicalRecordPart3Data", "IPDMedicalRecordPart3Id", "dbo.IPDMedicalRecordPart3");
            DropForeignKey("dbo.IPDMedicalRecords", "IPDMedicalRecordPart2Id", "dbo.IPDMedicalRecordPart2");
            DropForeignKey("dbo.IPDMedicalRecordPart2Data", "IPDMedicalRecordPart2Id", "dbo.IPDMedicalRecordPart2");
            DropForeignKey("dbo.IPDMedicalRecords", "IPDMedicalRecordPart1Id", "dbo.IPDMedicalRecordPart1");
            DropForeignKey("dbo.IPDMedicalRecordPart1Data", "IPDMedicalRecordPart1Id", "dbo.IPDMedicalRecordPart1");
            DropIndex("dbo.IPDMedicalRecordPart3Data", new[] { "IPDMedicalRecordPart3Id" });
            DropIndex("dbo.IPDMedicalRecordPart2Data", new[] { "IPDMedicalRecordPart2Id" });
            DropIndex("dbo.IPDMedicalRecordPart1Data", new[] { "IPDMedicalRecordPart1Id" });
            DropIndex("dbo.IPDMedicalRecords", new[] { "IPDMedicalRecordPart3Id" });
            DropIndex("dbo.IPDMedicalRecords", new[] { "IPDMedicalRecordPart2Id" });
            DropIndex("dbo.IPDMedicalRecords", new[] { "IPDMedicalRecordPart1Id" });
            DropIndex("dbo.IPDs", new[] { "IPDMedicalRecordId" });
            DropColumn("dbo.IPDs", "IPDMedicalRecordId");
            DropTable("dbo.IPDMedicalRecordPart3Data");
            DropTable("dbo.IPDMedicalRecordPart3");
            DropTable("dbo.IPDMedicalRecordPart2Data");
            DropTable("dbo.IPDMedicalRecordPart2");
            DropTable("dbo.IPDMedicalRecordPart1Data");
            DropTable("dbo.IPDMedicalRecordPart1");
            DropTable("dbo.IPDMedicalRecords");
        }
    }
}
