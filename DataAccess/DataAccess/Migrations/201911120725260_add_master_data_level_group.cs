namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_master_data_level_group : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MasterDatas", "EnName", c => c.String());
            AddColumn("dbo.MasterDatas", "Group", c => c.String());
            AddColumn("dbo.MasterDatas", "Level", c => c.Int());
            DropColumn("dbo.MasterDatas", "EngName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MasterDatas", "EngName", c => c.String());
            DropColumn("dbo.MasterDatas", "Level");
            DropColumn("dbo.MasterDatas", "Group");
            DropColumn("dbo.MasterDatas", "EnName");
        }
    }
}
