namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addchargetbls : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChargeDrafts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        PID = c.String(),
                        VisitCode = c.String(),
                        Note = c.String(),
                        RawData = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ChargeVisits", "VisitId", c => c.Guid());
            AddColumn("dbo.ChargeItemPathologies", "IsBirthControlMethod", c => c.Boolean(nullable: false));
            AddColumn("dbo.ChargeItemPathologies", "IsHormoneTreatment", c => c.Boolean(nullable: false));
            AddColumn("dbo.ChargeItemPathologies", "IsUterusremoval", c => c.Boolean(nullable: false));
            AddColumn("dbo.ChargeItemPathologies", "IsRadiationTheorapy", c => c.Boolean(nullable: false));
            AddColumn("dbo.ChargeItemPathologies", "IsPostMenopauseBleeding", c => c.Boolean(nullable: false));
            AddColumn("dbo.ChargeItemPathologies", "IsPregnant", c => c.Boolean(nullable: false));
            AddColumn("dbo.ChargeItemPathologies", "IsPostpartum", c => c.Boolean(nullable: false));
            AddColumn("dbo.ChargeItemPathologies", "IsBirthControlPills", c => c.Boolean(nullable: false));
            AddColumn("dbo.ChargeItemPathologies", "IsBreastfeeding", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ChargeItemPathologies", "IsBreastfeeding");
            DropColumn("dbo.ChargeItemPathologies", "IsBirthControlPills");
            DropColumn("dbo.ChargeItemPathologies", "IsPostpartum");
            DropColumn("dbo.ChargeItemPathologies", "IsPregnant");
            DropColumn("dbo.ChargeItemPathologies", "IsPostMenopauseBleeding");
            DropColumn("dbo.ChargeItemPathologies", "IsRadiationTheorapy");
            DropColumn("dbo.ChargeItemPathologies", "IsUterusremoval");
            DropColumn("dbo.ChargeItemPathologies", "IsHormoneTreatment");
            DropColumn("dbo.ChargeItemPathologies", "IsBirthControlMethod");
            DropColumn("dbo.ChargeVisits", "VisitId");
            DropTable("dbo.ChargeDrafts");
        }
    }
}
