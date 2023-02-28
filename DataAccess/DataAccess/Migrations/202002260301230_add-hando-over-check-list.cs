namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addhandooverchecklist : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OPDHandOverCheckListDatas",
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
                        EnValue = c.String(),
                        OPDHandOverCheckListId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OPDHandOverCheckLists", t => t.OPDHandOverCheckListId)
                .Index(t => t.OPDHandOverCheckListId);
            
            CreateTable(
                "dbo.OPDHandOverCheckLists",
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
                        ReasonForTransfer = c.String(),
                        HandOverTimePhysician = c.DateTime(),
                        HandOverPhysician = c.String(),
                        HandOverUnitPhysicianId = c.Guid(),
                        ReceivingPhysician = c.String(),
                        ReceivingUnitPhysicianId = c.Guid(),
                        IsAcceptPhysician = c.Boolean(nullable: false),
                        HandOverTimeNurse = c.DateTime(),
                        HandOverNurse = c.String(),
                        HandOverUnitNurseId = c.Guid(),
                        ReceivingNurse = c.String(),
                        ReceivingUnitNurseId = c.Guid(),
                        IsAcceptNurse = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Specialties", t => t.HandOverUnitNurseId)
                .ForeignKey("dbo.Specialties", t => t.HandOverUnitPhysicianId)
                .ForeignKey("dbo.Specialties", t => t.ReceivingUnitNurseId)
                .ForeignKey("dbo.Specialties", t => t.ReceivingUnitPhysicianId)
                .Index(t => t.HandOverUnitPhysicianId)
                .Index(t => t.ReceivingUnitPhysicianId)
                .Index(t => t.HandOverUnitNurseId)
                .Index(t => t.ReceivingUnitNurseId);
            
            AddColumn("dbo.OPDs", "OPDHandOverCheckList_Id", c => c.Guid());
            AddColumn("dbo.Sites", "Level", c => c.String());
            AddColumn("dbo.HandOverCheckLists", "HandOverUnitPhysicianId", c => c.Guid());
            AddColumn("dbo.HandOverCheckLists", "ReceivingUnitPhysicianId", c => c.Guid());
            AddColumn("dbo.HandOverCheckLists", "IsAcceptPhysician", c => c.Boolean(nullable: false));
            AddColumn("dbo.HandOverCheckLists", "HandOverUnitNurseId", c => c.Guid());
            AddColumn("dbo.HandOverCheckLists", "ReceivingUnitNurseId", c => c.Guid());
            AddColumn("dbo.HandOverCheckLists", "IsAcceptNurse", c => c.Boolean(nullable: false));
            CreateIndex("dbo.OPDs", "OPDHandOverCheckList_Id");
            CreateIndex("dbo.HandOverCheckLists", "HandOverUnitPhysicianId");
            CreateIndex("dbo.HandOverCheckLists", "ReceivingUnitPhysicianId");
            CreateIndex("dbo.HandOverCheckLists", "HandOverUnitNurseId");
            CreateIndex("dbo.HandOverCheckLists", "ReceivingUnitNurseId");
            AddForeignKey("dbo.HandOverCheckLists", "HandOverUnitNurseId", "dbo.Specialties", "Id");
            AddForeignKey("dbo.HandOverCheckLists", "HandOverUnitPhysicianId", "dbo.Specialties", "Id");
            AddForeignKey("dbo.HandOverCheckLists", "ReceivingUnitNurseId", "dbo.Specialties", "Id");
            AddForeignKey("dbo.HandOverCheckLists", "ReceivingUnitPhysicianId", "dbo.Specialties", "Id");
            AddForeignKey("dbo.OPDs", "OPDHandOverCheckList_Id", "dbo.OPDHandOverCheckLists", "Id");
            DropColumn("dbo.HandOverCheckLists", "HandOverUnitPhysician");
            DropColumn("dbo.HandOverCheckLists", "ReceivingUnitPhysician");
            DropColumn("dbo.HandOverCheckLists", "HandOverUnitNurse");
            DropColumn("dbo.HandOverCheckLists", "ReceivingUnitNurse");
        }
        
        public override void Down()
        {
            AddColumn("dbo.HandOverCheckLists", "ReceivingUnitNurse", c => c.String());
            AddColumn("dbo.HandOverCheckLists", "HandOverUnitNurse", c => c.String());
            AddColumn("dbo.HandOverCheckLists", "ReceivingUnitPhysician", c => c.String());
            AddColumn("dbo.HandOverCheckLists", "HandOverUnitPhysician", c => c.String());
            DropForeignKey("dbo.OPDHandOverCheckLists", "ReceivingUnitPhysicianId", "dbo.Specialties");
            DropForeignKey("dbo.OPDHandOverCheckLists", "ReceivingUnitNurseId", "dbo.Specialties");
            DropForeignKey("dbo.OPDs", "OPDHandOverCheckList_Id", "dbo.OPDHandOverCheckLists");
            DropForeignKey("dbo.OPDHandOverCheckListDatas", "OPDHandOverCheckListId", "dbo.OPDHandOverCheckLists");
            DropForeignKey("dbo.OPDHandOverCheckLists", "HandOverUnitPhysicianId", "dbo.Specialties");
            DropForeignKey("dbo.OPDHandOverCheckLists", "HandOverUnitNurseId", "dbo.Specialties");
            DropForeignKey("dbo.HandOverCheckLists", "ReceivingUnitPhysicianId", "dbo.Specialties");
            DropForeignKey("dbo.HandOverCheckLists", "ReceivingUnitNurseId", "dbo.Specialties");
            DropForeignKey("dbo.HandOverCheckLists", "HandOverUnitPhysicianId", "dbo.Specialties");
            DropForeignKey("dbo.HandOverCheckLists", "HandOverUnitNurseId", "dbo.Specialties");
            DropIndex("dbo.OPDHandOverCheckLists", new[] { "ReceivingUnitNurseId" });
            DropIndex("dbo.OPDHandOverCheckLists", new[] { "HandOverUnitNurseId" });
            DropIndex("dbo.OPDHandOverCheckLists", new[] { "ReceivingUnitPhysicianId" });
            DropIndex("dbo.OPDHandOverCheckLists", new[] { "HandOverUnitPhysicianId" });
            DropIndex("dbo.OPDHandOverCheckListDatas", new[] { "OPDHandOverCheckListId" });
            DropIndex("dbo.HandOverCheckLists", new[] { "ReceivingUnitNurseId" });
            DropIndex("dbo.HandOverCheckLists", new[] { "HandOverUnitNurseId" });
            DropIndex("dbo.HandOverCheckLists", new[] { "ReceivingUnitPhysicianId" });
            DropIndex("dbo.HandOverCheckLists", new[] { "HandOverUnitPhysicianId" });
            DropIndex("dbo.OPDs", new[] { "OPDHandOverCheckList_Id" });
            DropColumn("dbo.HandOverCheckLists", "IsAcceptNurse");
            DropColumn("dbo.HandOverCheckLists", "ReceivingUnitNurseId");
            DropColumn("dbo.HandOverCheckLists", "HandOverUnitNurseId");
            DropColumn("dbo.HandOverCheckLists", "IsAcceptPhysician");
            DropColumn("dbo.HandOverCheckLists", "ReceivingUnitPhysicianId");
            DropColumn("dbo.HandOverCheckLists", "HandOverUnitPhysicianId");
            DropColumn("dbo.Sites", "Level");
            DropColumn("dbo.OPDs", "OPDHandOverCheckList_Id");
            DropTable("dbo.OPDHandOverCheckLists");
            DropTable("dbo.OPDHandOverCheckListDatas");
        }
    }
}
