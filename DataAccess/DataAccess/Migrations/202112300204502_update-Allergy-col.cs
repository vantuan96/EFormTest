namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateAllergycol : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EDs", "IsAllergy", c => c.Boolean());
            AlterColumn("dbo.EOCs", "IsAllergy", c => c.Boolean());
            AlterColumn("dbo.IPDs", "IsAllergy", c => c.Boolean());
            AlterColumn("dbo.OPDs", "IsAllergy", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OPDs", "IsAllergy", c => c.Boolean(nullable: false));
            AlterColumn("dbo.IPDs", "IsAllergy", c => c.Boolean(nullable: false));
            AlterColumn("dbo.EOCs", "IsAllergy", c => c.Boolean(nullable: false));
            AlterColumn("dbo.EDs", "IsAllergy", c => c.Boolean(nullable: false));
        }
    }
}
