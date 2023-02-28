namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class observation_chart : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DischargeInformations",
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
                        AssessmentAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DischargeInformationDatas",
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
                        Code = c.String(),
                        Value = c.String(),
                        DischargeInfomationId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DischargeInformations", t => t.DischargeInfomationId)
                .Index(t => t.DischargeInfomationId);
            
            AddColumn("dbo.EDs", "VisitCode", c => c.String());
            AddColumn("dbo.EDs", "DischargeInformationId", c => c.Guid());
            CreateIndex("dbo.EDs", "DischargeInformationId");
            AddForeignKey("dbo.EDs", "DischargeInformationId", "dbo.DischargeInformations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EDs", "DischargeInformationId", "dbo.DischargeInformations");
            DropForeignKey("dbo.DischargeInformationDatas", "DischargeInfomationId", "dbo.DischargeInformations");
            DropIndex("dbo.DischargeInformationDatas", new[] { "DischargeInfomationId" });
            DropIndex("dbo.EDs", new[] { "DischargeInformationId" });
            DropColumn("dbo.EDs", "DischargeInformationId");
            DropColumn("dbo.EDs", "VisitCode");
            DropTable("dbo.DischargeInformationDatas");
            DropTable("dbo.DischargeInformations");
        }
    }
}
