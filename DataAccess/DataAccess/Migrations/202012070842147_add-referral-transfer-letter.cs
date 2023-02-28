namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addreferraltransferletter : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IPDReferralLetters",
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
                        PhysicianInChargeId = c.Guid(),
                        PhysicianInChargeTime = c.DateTime(),
                        DirectorId = c.Guid(),
                        DirectorTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.DirectorId)
                .ForeignKey("dbo.Users", t => t.PhysicianInChargeId)
                .Index(t => t.PhysicianInChargeId)
                .Index(t => t.DirectorId);
            
            CreateTable(
                "dbo.IPDTransferLetters",
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
                        PhysicianInChargeId = c.Guid(),
                        PhysicianInChargeTime = c.DateTime(),
                        DirectorId = c.Guid(),
                        DirectorTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.DirectorId)
                .ForeignKey("dbo.Users", t => t.PhysicianInChargeId)
                .Index(t => t.PhysicianInChargeId)
                .Index(t => t.DirectorId);
            
            AddColumn("dbo.IPDs", "IPDReferralLetterId", c => c.Guid());
            AddColumn("dbo.IPDs", "IPDTransferLetterId", c => c.Guid());
            CreateIndex("dbo.IPDs", "IPDReferralLetterId");
            CreateIndex("dbo.IPDs", "IPDTransferLetterId");
            AddForeignKey("dbo.IPDs", "IPDReferralLetterId", "dbo.IPDReferralLetters", "Id");
            AddForeignKey("dbo.IPDs", "IPDTransferLetterId", "dbo.IPDTransferLetters", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IPDs", "IPDTransferLetterId", "dbo.IPDTransferLetters");
            DropForeignKey("dbo.IPDTransferLetters", "PhysicianInChargeId", "dbo.Users");
            DropForeignKey("dbo.IPDTransferLetters", "DirectorId", "dbo.Users");
            DropForeignKey("dbo.IPDs", "IPDReferralLetterId", "dbo.IPDReferralLetters");
            DropForeignKey("dbo.IPDReferralLetters", "PhysicianInChargeId", "dbo.Users");
            DropForeignKey("dbo.IPDReferralLetters", "DirectorId", "dbo.Users");
            DropIndex("dbo.IPDTransferLetters", new[] { "DirectorId" });
            DropIndex("dbo.IPDTransferLetters", new[] { "PhysicianInChargeId" });
            DropIndex("dbo.IPDReferralLetters", new[] { "DirectorId" });
            DropIndex("dbo.IPDReferralLetters", new[] { "PhysicianInChargeId" });
            DropIndex("dbo.IPDs", new[] { "IPDTransferLetterId" });
            DropIndex("dbo.IPDs", new[] { "IPDReferralLetterId" });
            DropColumn("dbo.IPDs", "IPDTransferLetterId");
            DropColumn("dbo.IPDs", "IPDReferralLetterId");
            DropTable("dbo.IPDTransferLetters");
            DropTable("dbo.IPDReferralLetters");
        }
    }
}
