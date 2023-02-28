namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatemddatatbl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MasterDatas", "DefaultValue", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MasterDatas", "DefaultValue");
        }
    }
}
