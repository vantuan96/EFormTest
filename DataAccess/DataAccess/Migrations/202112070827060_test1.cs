namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ChargeVisits", "VisitId");
            DropColumn("dbo.ChargeItemPathologies", "IsBirthControlMethod");
            DropColumn("dbo.ChargeItemPathologies", "IsHormoneTreatment");
            DropColumn("dbo.ChargeItemPathologies", "IsUterusremoval");
            DropColumn("dbo.ChargeItemPathologies", "IsRadiationTheorapy");
            DropColumn("dbo.ChargeItemPathologies", "IsPostMenopauseBleeding");
            DropColumn("dbo.ChargeItemPathologies", "IsPregnant");
            DropColumn("dbo.ChargeItemPathologies", "IsPostpartum");
            DropColumn("dbo.ChargeItemPathologies", "IsBirthControlPills");
            DropColumn("dbo.ChargeItemPathologies", "IsBreastfeeding");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ChargeItemPathologies", "IsBreastfeeding", c => c.Boolean(nullable: false));
            AddColumn("dbo.ChargeItemPathologies", "IsBirthControlPills", c => c.Boolean(nullable: false));
            AddColumn("dbo.ChargeItemPathologies", "IsPostpartum", c => c.Boolean(nullable: false));
            AddColumn("dbo.ChargeItemPathologies", "IsPregnant", c => c.Boolean(nullable: false));
            AddColumn("dbo.ChargeItemPathologies", "IsPostMenopauseBleeding", c => c.Boolean(nullable: false));
            AddColumn("dbo.ChargeItemPathologies", "IsRadiationTheorapy", c => c.Boolean(nullable: false));
            AddColumn("dbo.ChargeItemPathologies", "IsUterusremoval", c => c.Boolean(nullable: false));
            AddColumn("dbo.ChargeItemPathologies", "IsHormoneTreatment", c => c.Boolean(nullable: false));
            AddColumn("dbo.ChargeItemPathologies", "IsBirthControlMethod", c => c.Boolean(nullable: false));
            AddColumn("dbo.ChargeVisits", "VisitId", c => c.Guid());
        }
    }
}
