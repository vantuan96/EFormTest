namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changlog : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Logs", "Response", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Logs", "Response", c => c.String(unicode: false, storeType: "text"));
        }
    }
}
