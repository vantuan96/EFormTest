namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addhandovercheck : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HandOverCheckLists",
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
                        HandOverTime = c.DateTime(),
                        HandOverPhysician = c.String(),
                        HandOverUnit = c.String(),
                        ReceivingPhysician = c.String(),
                        ReceivingUnit = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HandOverCheckListDatas",
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
                        HandOverCheckListId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HandOverCheckLists", t => t.HandOverCheckListId)
                .Index(t => t.HandOverCheckListId);
            
            AddColumn("dbo.EDs", "HandOverCheckListId", c => c.Guid());
            AddColumn("dbo.DischargeInformationDatas", "EnValue", c => c.String());
            AddColumn("dbo.EmergencyRecordDatas", "EnValue", c => c.String());
            AddColumn("dbo.EmergencyTriageRecordDatas", "EnValue", c => c.String());
            CreateIndex("dbo.EDs", "HandOverCheckListId");
            AddForeignKey("dbo.EDs", "HandOverCheckListId", "dbo.HandOverCheckLists", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HandOverCheckListDatas", "HandOverCheckListId", "dbo.HandOverCheckLists");
            DropForeignKey("dbo.EDs", "HandOverCheckListId", "dbo.HandOverCheckLists");
            DropIndex("dbo.HandOverCheckListDatas", new[] { "HandOverCheckListId" });
            DropIndex("dbo.EDs", new[] { "HandOverCheckListId" });
            DropColumn("dbo.EmergencyTriageRecordDatas", "EnValue");
            DropColumn("dbo.EmergencyRecordDatas", "EnValue");
            DropColumn("dbo.DischargeInformationDatas", "EnValue");
            DropColumn("dbo.EDs", "HandOverCheckListId");
            DropTable("dbo.HandOverCheckListDatas");
            DropTable("dbo.HandOverCheckLists");
        }
    }
}
