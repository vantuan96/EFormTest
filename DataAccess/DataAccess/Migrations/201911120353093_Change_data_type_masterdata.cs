namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Change_data_type_masterdata : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MasterDatas", "DataType", c => c.String());
            DropColumn("dbo.MasterDatas", "Data_type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MasterDatas", "Data_type", c => c.String());
            DropColumn("dbo.MasterDatas", "DataType");
        }
    }
}
