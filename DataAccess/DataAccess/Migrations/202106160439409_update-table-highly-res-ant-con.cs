namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetablehighlyresantcon : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.HighlyRestrictedAntimicrobialConsults", "DiagnosisOfInfection");
            DropColumn("dbo.HighlyRestrictedAntimicrobialConsults", "DiagnosisOfInfectionOther");
            DropColumn("dbo.HighlyRestrictedAntimicrobialConsults", "ClinicalSymtoms");
            DropColumn("dbo.HighlyRestrictedAntimicrobialConsults", "CurrentAntimicrobials");
            DropColumn("dbo.HighlyRestrictedAntimicrobialConsults", "IndicationsOfHighlyRestrictedAntimicrobials");
            DropColumn("dbo.HighlyRestrictedAntimicrobialConsults", "IndicationsOfHighlyRestrictedAntimicrobialsOther");
            DropColumn("dbo.HighlyRestrictedAntimicrobialConsults", "IsAllergy");
            DropColumn("dbo.HighlyRestrictedAntimicrobialConsults", "Allergy");
            DropColumn("dbo.HighlyRestrictedAntimicrobialConsults", "KindOfAllergy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.HighlyRestrictedAntimicrobialConsults", "KindOfAllergy", c => c.String());
            AddColumn("dbo.HighlyRestrictedAntimicrobialConsults", "Allergy", c => c.String());
            AddColumn("dbo.HighlyRestrictedAntimicrobialConsults", "IsAllergy", c => c.Boolean(nullable: false));
            AddColumn("dbo.HighlyRestrictedAntimicrobialConsults", "IndicationsOfHighlyRestrictedAntimicrobialsOther", c => c.String());
            AddColumn("dbo.HighlyRestrictedAntimicrobialConsults", "IndicationsOfHighlyRestrictedAntimicrobials", c => c.String());
            AddColumn("dbo.HighlyRestrictedAntimicrobialConsults", "CurrentAntimicrobials", c => c.String());
            AddColumn("dbo.HighlyRestrictedAntimicrobialConsults", "ClinicalSymtoms", c => c.String());
            AddColumn("dbo.HighlyRestrictedAntimicrobialConsults", "DiagnosisOfInfectionOther", c => c.String());
            AddColumn("dbo.HighlyRestrictedAntimicrobialConsults", "DiagnosisOfInfection", c => c.String());
        }
    }
}
