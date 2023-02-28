namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update3translation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Translations", "ToLanguage", c => c.String());
            AddColumn("dbo.Translations", "FromLanguage", c => c.String());
            DropColumn("dbo.Translations", "Language");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Translations", "Language", c => c.String());
            DropColumn("dbo.Translations", "FromLanguage");
            DropColumn("dbo.Translations", "ToLanguage");
        }
    }
}
