namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_note : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MasterDatas", "Note", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MasterDatas", "Note");
        }
    }
}
