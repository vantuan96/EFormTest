namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatepatientownermedicationchart : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EIOPatientOwnMedicationsChartDatas", "StorageOrigin", c => c.String());
            AddColumn("dbo.EIOPatientOwnMedicationsCharts", "StorageDrugsAtPharmacy", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EIOPatientOwnMedicationsCharts", "StorageDrugsAtPharmacy");
            DropColumn("dbo.EIOPatientOwnMedicationsChartDatas", "StorageOrigin");
        }
    }
}
