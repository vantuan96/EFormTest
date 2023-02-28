namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatehandoverchecklist2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OPDHandOverCheckLists", "HandOverPhysicianId", c => c.Guid());
            AddColumn("dbo.OPDHandOverCheckLists", "ReceivingPhysicianId", c => c.Guid());
            AddColumn("dbo.OPDHandOverCheckLists", "HandOverNurseId", c => c.Guid());
            AddColumn("dbo.OPDHandOverCheckLists", "ReceivingNurseId", c => c.Guid());
            AddColumn("dbo.HandOverCheckLists", "HandOverPhysicianId", c => c.Guid());
            AddColumn("dbo.HandOverCheckLists", "ReceivingPhysicianId", c => c.Guid());
            AddColumn("dbo.HandOverCheckLists", "HandOverNurseId", c => c.Guid());
            AddColumn("dbo.HandOverCheckLists", "ReceivingNurseId", c => c.Guid());
            CreateIndex("dbo.OPDHandOverCheckLists", "HandOverPhysicianId");
            CreateIndex("dbo.OPDHandOverCheckLists", "ReceivingPhysicianId");
            CreateIndex("dbo.OPDHandOverCheckLists", "HandOverNurseId");
            CreateIndex("dbo.OPDHandOverCheckLists", "ReceivingNurseId");
            CreateIndex("dbo.HandOverCheckLists", "HandOverPhysicianId");
            CreateIndex("dbo.HandOverCheckLists", "ReceivingPhysicianId");
            CreateIndex("dbo.HandOverCheckLists", "HandOverNurseId");
            CreateIndex("dbo.HandOverCheckLists", "ReceivingNurseId");
            AddForeignKey("dbo.OPDHandOverCheckLists", "HandOverNurseId", "dbo.Users", "Id");
            AddForeignKey("dbo.OPDHandOverCheckLists", "HandOverPhysicianId", "dbo.Users", "Id");
            AddForeignKey("dbo.OPDHandOverCheckLists", "ReceivingNurseId", "dbo.Users", "Id");
            AddForeignKey("dbo.OPDHandOverCheckLists", "ReceivingPhysicianId", "dbo.Users", "Id");
            AddForeignKey("dbo.HandOverCheckLists", "HandOverNurseId", "dbo.Users", "Id");
            AddForeignKey("dbo.HandOverCheckLists", "HandOverPhysicianId", "dbo.Users", "Id");
            AddForeignKey("dbo.HandOverCheckLists", "ReceivingNurseId", "dbo.Users", "Id");
            AddForeignKey("dbo.HandOverCheckLists", "ReceivingPhysicianId", "dbo.Users", "Id");
            DropColumn("dbo.OPDHandOverCheckLists", "HandOverPhysician");
            DropColumn("dbo.OPDHandOverCheckLists", "ReceivingPhysician");
            DropColumn("dbo.OPDHandOverCheckLists", "HandOverNurse");
            DropColumn("dbo.OPDHandOverCheckLists", "ReceivingNurse");
            DropColumn("dbo.HandOverCheckLists", "HandOverPhysician");
            DropColumn("dbo.HandOverCheckLists", "ReceivingPhysician");
            DropColumn("dbo.HandOverCheckLists", "HandOverNurse");
            DropColumn("dbo.HandOverCheckLists", "ReceivingNurse");
        }
        
        public override void Down()
        {
            AddColumn("dbo.HandOverCheckLists", "ReceivingNurse", c => c.String());
            AddColumn("dbo.HandOverCheckLists", "HandOverNurse", c => c.String());
            AddColumn("dbo.HandOverCheckLists", "ReceivingPhysician", c => c.String());
            AddColumn("dbo.HandOverCheckLists", "HandOverPhysician", c => c.String());
            AddColumn("dbo.OPDHandOverCheckLists", "ReceivingNurse", c => c.String());
            AddColumn("dbo.OPDHandOverCheckLists", "HandOverNurse", c => c.String());
            AddColumn("dbo.OPDHandOverCheckLists", "ReceivingPhysician", c => c.String());
            AddColumn("dbo.OPDHandOverCheckLists", "HandOverPhysician", c => c.String());
            DropForeignKey("dbo.HandOverCheckLists", "ReceivingPhysicianId", "dbo.Users");
            DropForeignKey("dbo.HandOverCheckLists", "ReceivingNurseId", "dbo.Users");
            DropForeignKey("dbo.HandOverCheckLists", "HandOverPhysicianId", "dbo.Users");
            DropForeignKey("dbo.HandOverCheckLists", "HandOverNurseId", "dbo.Users");
            DropForeignKey("dbo.OPDHandOverCheckLists", "ReceivingPhysicianId", "dbo.Users");
            DropForeignKey("dbo.OPDHandOverCheckLists", "ReceivingNurseId", "dbo.Users");
            DropForeignKey("dbo.OPDHandOverCheckLists", "HandOverPhysicianId", "dbo.Users");
            DropForeignKey("dbo.OPDHandOverCheckLists", "HandOverNurseId", "dbo.Users");
            DropIndex("dbo.HandOverCheckLists", new[] { "ReceivingNurseId" });
            DropIndex("dbo.HandOverCheckLists", new[] { "HandOverNurseId" });
            DropIndex("dbo.HandOverCheckLists", new[] { "ReceivingPhysicianId" });
            DropIndex("dbo.HandOverCheckLists", new[] { "HandOverPhysicianId" });
            DropIndex("dbo.OPDHandOverCheckLists", new[] { "ReceivingNurseId" });
            DropIndex("dbo.OPDHandOverCheckLists", new[] { "HandOverNurseId" });
            DropIndex("dbo.OPDHandOverCheckLists", new[] { "ReceivingPhysicianId" });
            DropIndex("dbo.OPDHandOverCheckLists", new[] { "HandOverPhysicianId" });
            DropColumn("dbo.HandOverCheckLists", "ReceivingNurseId");
            DropColumn("dbo.HandOverCheckLists", "HandOverNurseId");
            DropColumn("dbo.HandOverCheckLists", "ReceivingPhysicianId");
            DropColumn("dbo.HandOverCheckLists", "HandOverPhysicianId");
            DropColumn("dbo.OPDHandOverCheckLists", "ReceivingNurseId");
            DropColumn("dbo.OPDHandOverCheckLists", "HandOverNurseId");
            DropColumn("dbo.OPDHandOverCheckLists", "ReceivingPhysicianId");
            DropColumn("dbo.OPDHandOverCheckLists", "HandOverPhysicianId");
        }
    }
}
