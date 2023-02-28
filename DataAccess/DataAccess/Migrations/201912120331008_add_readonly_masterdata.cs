namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_readonly_masterdata : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MasterDatas", "IsReadOnly", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MasterDatas", "IsReadOnly");
        }
    }
}
