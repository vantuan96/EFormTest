namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnewAllergycol : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EDs", "IsAllergy", c => c.Boolean(nullable: false));
            AddColumn("dbo.EDs", "Allergy", c => c.String());
            AddColumn("dbo.EDs", "KindOfAllergy", c => c.String());
            AddColumn("dbo.EOCs", "IsAllergy", c => c.Boolean(nullable: false));
            AddColumn("dbo.EOCs", "Allergy", c => c.String());
            AddColumn("dbo.EOCs", "KindOfAllergy", c => c.String());
            AddColumn("dbo.IPDs", "IsAllergy", c => c.Boolean(nullable: false));
            AddColumn("dbo.IPDs", "Allergy", c => c.String());
            AddColumn("dbo.IPDs", "KindOfAllergy", c => c.String());
            AddColumn("dbo.OPDs", "IsAllergy", c => c.Boolean(nullable: false));
            AddColumn("dbo.OPDs", "Allergy", c => c.String());
            AddColumn("dbo.OPDs", "KindOfAllergy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OPDs", "KindOfAllergy");
            DropColumn("dbo.OPDs", "Allergy");
            DropColumn("dbo.OPDs", "IsAllergy");
            DropColumn("dbo.IPDs", "KindOfAllergy");
            DropColumn("dbo.IPDs", "Allergy");
            DropColumn("dbo.IPDs", "IsAllergy");
            DropColumn("dbo.EOCs", "KindOfAllergy");
            DropColumn("dbo.EOCs", "Allergy");
            DropColumn("dbo.EOCs", "IsAllergy");
            DropColumn("dbo.EDs", "KindOfAllergy");
            DropColumn("dbo.EDs", "Allergy");
            DropColumn("dbo.EDs", "IsAllergy");
        }
    }
}
