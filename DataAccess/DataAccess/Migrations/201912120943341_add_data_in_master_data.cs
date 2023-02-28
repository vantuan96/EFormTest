namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_data_in_master_data : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MasterDatas", "Data", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MasterDatas", "Data");
        }
    }
}
