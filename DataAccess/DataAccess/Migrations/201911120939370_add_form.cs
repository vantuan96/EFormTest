namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_form : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MasterDatas", "Form", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MasterDatas", "Form");
        }
    }
}
