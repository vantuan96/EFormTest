namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetrans4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TranslationDatas", "Code", c => c.String());
            AddColumn("dbo.TranslationDatas", "Value", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TranslationDatas", "Value");
            DropColumn("dbo.TranslationDatas", "Code");
        }
    }
}
