namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update17112022 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppVersions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Order = c.Int(nullable: false, identity: true),
                        Lable = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Version = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.EDs", "Version", c => c.Int(nullable: false));
            AddColumn("dbo.EDHandOverCheckLists", "NurseAcceptTime", c => c.DateTime());
            AddColumn("dbo.EDHandOverCheckLists", "PhysicianAcceptTime", c => c.DateTime());
            AddColumn("dbo.EIOMortalityReportMembers", "IsNotMember", c => c.Boolean(nullable: false));
            AddColumn("dbo.EOCs", "Version", c => c.Int(nullable: false));
            AddColumn("dbo.EOCs", "IsHasFallRiskScreening", c => c.Boolean(nullable: false));
            AddColumn("dbo.EOCHandOverCheckLists", "NurseAcceptTime", c => c.DateTime());
            AddColumn("dbo.EOCHandOverCheckLists", "PhysicianAcceptTime", c => c.DateTime());
            AddColumn("dbo.IPDs", "Version", c => c.Int(nullable: false));
            AddColumn("dbo.IPDHandOverCheckLists", "NurseAcceptTime", c => c.DateTime());
            AddColumn("dbo.IPDHandOverCheckLists", "PhysicianAcceptTime", c => c.DateTime());
            AddColumn("dbo.OPDHandOverCheckLists", "NurseAcceptTime", c => c.DateTime());
            AddColumn("dbo.OPDHandOverCheckLists", "PhysicianAcceptTime", c => c.DateTime());
            AddColumn("dbo.OPDs", "Version", c => c.Int(nullable: false));
            AddColumn("dbo.UploadImages", "FormId", c => c.Guid(nullable: false));
            AddColumn("dbo.UploadImages", "FormCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UploadImages", "FormCode");
            DropColumn("dbo.UploadImages", "FormId");
            DropColumn("dbo.OPDs", "Version");
            DropColumn("dbo.OPDHandOverCheckLists", "PhysicianAcceptTime");
            DropColumn("dbo.OPDHandOverCheckLists", "NurseAcceptTime");
            DropColumn("dbo.IPDHandOverCheckLists", "PhysicianAcceptTime");
            DropColumn("dbo.IPDHandOverCheckLists", "NurseAcceptTime");
            DropColumn("dbo.IPDs", "Version");
            DropColumn("dbo.EOCHandOverCheckLists", "PhysicianAcceptTime");
            DropColumn("dbo.EOCHandOverCheckLists", "NurseAcceptTime");
            DropColumn("dbo.EOCs", "IsHasFallRiskScreening");
            DropColumn("dbo.EOCs", "Version");
            DropColumn("dbo.EIOMortalityReportMembers", "IsNotMember");
            DropColumn("dbo.EDHandOverCheckLists", "PhysicianAcceptTime");
            DropColumn("dbo.EDHandOverCheckLists", "NurseAcceptTime");
            DropColumn("dbo.EDs", "Version");
            DropTable("dbo.AppVersions");
        }
    }
}
