namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetralation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Translations", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Translations", "Status", c => c.String());
        }
    }
}
