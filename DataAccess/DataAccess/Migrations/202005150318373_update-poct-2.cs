namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatepoct2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EDs", "EDArterialBloodGasTestId", c => c.Guid());
            AddColumn("dbo.EDs", "EDChemicalBiologyTestId", c => c.Guid());
            AddColumn("dbo.EDArterialBloodGasTests", "ExecutionUserId", c => c.Guid());
            AddColumn("dbo.EDArterialBloodGasTests", "DoctorAcceptId", c => c.Guid());
            AddColumn("dbo.EDChemicalBiologyTests", "ExecutionUserId", c => c.Guid());
            AddColumn("dbo.EDChemicalBiologyTests", "DoctorAcceptId", c => c.Guid());
            CreateIndex("dbo.EDs", "EDArterialBloodGasTestId");
            CreateIndex("dbo.EDs", "EDChemicalBiologyTestId");
            CreateIndex("dbo.EDArterialBloodGasTests", "ExecutionUserId");
            CreateIndex("dbo.EDArterialBloodGasTests", "DoctorAcceptId");
            CreateIndex("dbo.EDChemicalBiologyTests", "ExecutionUserId");
            CreateIndex("dbo.EDChemicalBiologyTests", "DoctorAcceptId");
            AddForeignKey("dbo.EDArterialBloodGasTests", "DoctorAcceptId", "dbo.Users", "Id");
            AddForeignKey("dbo.EDArterialBloodGasTests", "ExecutionUserId", "dbo.Users", "Id");
            AddForeignKey("dbo.EDs", "EDArterialBloodGasTestId", "dbo.EDArterialBloodGasTests", "Id");
            AddForeignKey("dbo.EDChemicalBiologyTests", "DoctorAcceptId", "dbo.Users", "Id");
            AddForeignKey("dbo.EDChemicalBiologyTests", "ExecutionUserId", "dbo.Users", "Id");
            AddForeignKey("dbo.EDs", "EDChemicalBiologyTestId", "dbo.EDChemicalBiologyTests", "Id");
            DropColumn("dbo.EDArterialBloodGasTests", "ExecutionUser");
            DropColumn("dbo.EDArterialBloodGasTests", "DoctorAccept");
            DropColumn("dbo.EDChemicalBiologyTests", "ExecutionUser");
            DropColumn("dbo.EDChemicalBiologyTests", "DoctorAccept");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EDChemicalBiologyTests", "DoctorAccept", c => c.Guid());
            AddColumn("dbo.EDChemicalBiologyTests", "ExecutionUser", c => c.Guid());
            AddColumn("dbo.EDArterialBloodGasTests", "DoctorAccept", c => c.Guid());
            AddColumn("dbo.EDArterialBloodGasTests", "ExecutionUser", c => c.Guid());
            DropForeignKey("dbo.EDs", "EDChemicalBiologyTestId", "dbo.EDChemicalBiologyTests");
            DropForeignKey("dbo.EDChemicalBiologyTests", "ExecutionUserId", "dbo.Users");
            DropForeignKey("dbo.EDChemicalBiologyTests", "DoctorAcceptId", "dbo.Users");
            DropForeignKey("dbo.EDs", "EDArterialBloodGasTestId", "dbo.EDArterialBloodGasTests");
            DropForeignKey("dbo.EDArterialBloodGasTests", "ExecutionUserId", "dbo.Users");
            DropForeignKey("dbo.EDArterialBloodGasTests", "DoctorAcceptId", "dbo.Users");
            DropIndex("dbo.EDChemicalBiologyTests", new[] { "DoctorAcceptId" });
            DropIndex("dbo.EDChemicalBiologyTests", new[] { "ExecutionUserId" });
            DropIndex("dbo.EDArterialBloodGasTests", new[] { "DoctorAcceptId" });
            DropIndex("dbo.EDArterialBloodGasTests", new[] { "ExecutionUserId" });
            DropIndex("dbo.EDs", new[] { "EDChemicalBiologyTestId" });
            DropIndex("dbo.EDs", new[] { "EDArterialBloodGasTestId" });
            DropColumn("dbo.EDChemicalBiologyTests", "DoctorAcceptId");
            DropColumn("dbo.EDChemicalBiologyTests", "ExecutionUserId");
            DropColumn("dbo.EDArterialBloodGasTests", "DoctorAcceptId");
            DropColumn("dbo.EDArterialBloodGasTests", "ExecutionUserId");
            DropColumn("dbo.EDs", "EDChemicalBiologyTestId");
            DropColumn("dbo.EDs", "EDArterialBloodGasTestId");
        }
    }
}
