namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addstandingorderforretailservice : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MedicationMasterdatas",
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
                        Content = c.String(),
                        IsStop = c.Boolean(nullable: false),
                        Manufactory = c.String(),
                        Name = c.String(),
                        AutoComplete = c.String(),
                        ActiveMaterial = c.String(),
                        System = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EDStandingOrderForRetailServices",
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
                        Doctor = c.String(),
                        Pulse = c.String(),
                        Temparature = c.String(),
                        BloodPressure = c.String(),
                        Height = c.String(),
                        Weight = c.String(),
                        Diagnosis = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Orders", "Speed", c => c.String());
            AddColumn("dbo.Orders", "MedicationMasterdataId", c => c.Guid());
            AddColumn("dbo.EDs", "EDStandingOrderForRetailServiceId", c => c.Guid());
            CreateIndex("dbo.Orders", "MedicationMasterdataId");
            CreateIndex("dbo.EDs", "EDStandingOrderForRetailServiceId");
            AddForeignKey("dbo.Orders", "MedicationMasterdataId", "dbo.MedicationMasterdatas", "Id");
            AddForeignKey("dbo.EDs", "EDStandingOrderForRetailServiceId", "dbo.EDStandingOrderForRetailServices", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EDs", "EDStandingOrderForRetailServiceId", "dbo.EDStandingOrderForRetailServices");
            DropForeignKey("dbo.Orders", "MedicationMasterdataId", "dbo.MedicationMasterdatas");
            DropIndex("dbo.EDs", new[] { "EDStandingOrderForRetailServiceId" });
            DropIndex("dbo.Orders", new[] { "MedicationMasterdataId" });
            DropColumn("dbo.EDs", "EDStandingOrderForRetailServiceId");
            DropColumn("dbo.Orders", "MedicationMasterdataId");
            DropColumn("dbo.Orders", "Speed");
            DropTable("dbo.EDStandingOrderForRetailServices");
            DropTable("dbo.MedicationMasterdatas");
        }
    }
}
