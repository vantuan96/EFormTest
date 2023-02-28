namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatestatustable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EDStatus", "Code", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EDStatus", "Code");
        }
    }
}
