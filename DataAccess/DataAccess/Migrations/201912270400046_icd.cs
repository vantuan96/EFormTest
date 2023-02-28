namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class icd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ICD10",
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
                        EnName = c.String(),
                        ViName = c.String(),
                        ViNameWithoutSign = c.String(),
                        Code = c.String(),
                        GroupCode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.HandOverCheckLists", "HandOverTimePhysician", c => c.DateTime());
            AddColumn("dbo.HandOverCheckLists", "HandOverUnitPhysician", c => c.String());
            AddColumn("dbo.HandOverCheckLists", "ReceivingUnitPhysician", c => c.String());
            AddColumn("dbo.HandOverCheckLists", "HandOverTimeNurse", c => c.DateTime());
            AddColumn("dbo.HandOverCheckLists", "HandOverNurse", c => c.String());
            AddColumn("dbo.HandOverCheckLists", "HandOverUnitNurse", c => c.String());
            AddColumn("dbo.HandOverCheckLists", "ReceivingNurse", c => c.String());
            AddColumn("dbo.HandOverCheckLists", "ReceivingUnitNurse", c => c.String());
            DropColumn("dbo.HandOverCheckLists", "HandOverTime");
            DropColumn("dbo.HandOverCheckLists", "HandOverUnit");
            DropColumn("dbo.HandOverCheckLists", "ReceivingUnit");
        }
        
        public override void Down()
        {
            AddColumn("dbo.HandOverCheckLists", "ReceivingUnit", c => c.String());
            AddColumn("dbo.HandOverCheckLists", "HandOverUnit", c => c.String());
            AddColumn("dbo.HandOverCheckLists", "HandOverTime", c => c.DateTime());
            DropColumn("dbo.HandOverCheckLists", "ReceivingUnitNurse");
            DropColumn("dbo.HandOverCheckLists", "ReceivingNurse");
            DropColumn("dbo.HandOverCheckLists", "HandOverUnitNurse");
            DropColumn("dbo.HandOverCheckLists", "HandOverNurse");
            DropColumn("dbo.HandOverCheckLists", "HandOverTimeNurse");
            DropColumn("dbo.HandOverCheckLists", "ReceivingUnitPhysician");
            DropColumn("dbo.HandOverCheckLists", "HandOverUnitPhysician");
            DropColumn("dbo.HandOverCheckLists", "HandOverTimePhysician");
            DropTable("dbo.ICD10");
        }
    }
}
