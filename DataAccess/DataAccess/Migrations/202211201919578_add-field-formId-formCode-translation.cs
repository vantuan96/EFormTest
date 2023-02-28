namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfieldformIdformCodetranslation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Translations", "FormId", c => c.Guid());
            AddColumn("dbo.Translations", "FormCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Translations", "FormCode");
            DropColumn("dbo.Translations", "FormId");
        }
    }
}
