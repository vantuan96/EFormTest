namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initEOCHandOverCheckList : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EOCHandOverCheckLists",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ReasonForTransfer = c.String(),
                        HandOverTimePhysician = c.DateTime(),
                        HandOverPhysicianId = c.Guid(),
                        HandOverUnitPhysicianId = c.Guid(),
                        ReceivingPhysicianId = c.Guid(),
                        ReceivingUnitPhysicianId = c.Guid(),
                        IsAcceptPhysician = c.Boolean(nullable: false),
                        HandOverTimeNurse = c.DateTime(),
                        HandOverNurseId = c.Guid(),
                        HandOverUnitNurseId = c.Guid(),
                        ReceivingNurseId = c.Guid(),
                        ReceivingUnitNurseId = c.Guid(),
                        IsAcceptNurse = c.Boolean(nullable: false),
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
                .ForeignKey("dbo.Users", t => t.HandOverNurseId)
                .ForeignKey("dbo.Users", t => t.HandOverPhysicianId)
                .ForeignKey("dbo.Specialties", t => t.HandOverUnitNurseId)
                .ForeignKey("dbo.Specialties", t => t.HandOverUnitPhysicianId)
                .ForeignKey("dbo.Users", t => t.ReceivingNurseId)
                .ForeignKey("dbo.Users", t => t.ReceivingPhysicianId)
                .ForeignKey("dbo.Specialties", t => t.ReceivingUnitNurseId)
                .ForeignKey("dbo.Specialties", t => t.ReceivingUnitPhysicianId)
                .ForeignKey("dbo.EOCs", t => t.VisitId)
                .Index(t => t.HandOverPhysicianId)
                .Index(t => t.HandOverUnitPhysicianId)
                .Index(t => t.ReceivingPhysicianId)
                .Index(t => t.ReceivingUnitPhysicianId)
                .Index(t => t.HandOverNurseId)
                .Index(t => t.HandOverUnitNurseId)
                .Index(t => t.ReceivingNurseId)
                .Index(t => t.ReceivingUnitNurseId)
                .Index(t => t.VisitId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EOCHandOverCheckLists", "VisitId", "dbo.EOCs");
            DropForeignKey("dbo.EOCHandOverCheckLists", "ReceivingUnitPhysicianId", "dbo.Specialties");
            DropForeignKey("dbo.EOCHandOverCheckLists", "ReceivingUnitNurseId", "dbo.Specialties");
            DropForeignKey("dbo.EOCHandOverCheckLists", "ReceivingPhysicianId", "dbo.Users");
            DropForeignKey("dbo.EOCHandOverCheckLists", "ReceivingNurseId", "dbo.Users");
            DropForeignKey("dbo.EOCHandOverCheckLists", "HandOverUnitPhysicianId", "dbo.Specialties");
            DropForeignKey("dbo.EOCHandOverCheckLists", "HandOverUnitNurseId", "dbo.Specialties");
            DropForeignKey("dbo.EOCHandOverCheckLists", "HandOverPhysicianId", "dbo.Users");
            DropForeignKey("dbo.EOCHandOverCheckLists", "HandOverNurseId", "dbo.Users");
            DropIndex("dbo.EOCHandOverCheckLists", new[] { "VisitId" });
            DropIndex("dbo.EOCHandOverCheckLists", new[] { "ReceivingUnitNurseId" });
            DropIndex("dbo.EOCHandOverCheckLists", new[] { "ReceivingNurseId" });
            DropIndex("dbo.EOCHandOverCheckLists", new[] { "HandOverUnitNurseId" });
            DropIndex("dbo.EOCHandOverCheckLists", new[] { "HandOverNurseId" });
            DropIndex("dbo.EOCHandOverCheckLists", new[] { "ReceivingUnitPhysicianId" });
            DropIndex("dbo.EOCHandOverCheckLists", new[] { "ReceivingPhysicianId" });
            DropIndex("dbo.EOCHandOverCheckLists", new[] { "HandOverUnitPhysicianId" });
            DropIndex("dbo.EOCHandOverCheckLists", new[] { "HandOverPhysicianId" });
            DropTable("dbo.EOCHandOverCheckLists");
        }
    }
}
