namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addehosaccount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "EHOSAccount", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "EHOSAccount");
        }
    }
}
