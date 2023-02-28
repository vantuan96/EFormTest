namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatemohcustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "MOHJob", c => c.String());
            AddColumn("dbo.Customers", "MOHJobCode", c => c.String());
            AddColumn("dbo.Customers", "MOHEthnic", c => c.String());
            AddColumn("dbo.Customers", "MOHEthnicCode", c => c.String());
            AddColumn("dbo.Customers", "MOHNationality", c => c.String());
            AddColumn("dbo.Customers", "MOHNationalityCode", c => c.String());
            AddColumn("dbo.Customers", "MOHProvince", c => c.String());
            AddColumn("dbo.Customers", "MOHProvinceCode", c => c.String());
            AddColumn("dbo.Customers", "MOHDistrict", c => c.String());
            AddColumn("dbo.Customers", "MOHDistrictCode", c => c.String());
            AddColumn("dbo.Customers", "MOHObject", c => c.String());
            AddColumn("dbo.Customers", "MOHObjectOther", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "MOHObjectOther");
            DropColumn("dbo.Customers", "MOHObject");
            DropColumn("dbo.Customers", "MOHDistrictCode");
            DropColumn("dbo.Customers", "MOHDistrict");
            DropColumn("dbo.Customers", "MOHProvinceCode");
            DropColumn("dbo.Customers", "MOHProvince");
            DropColumn("dbo.Customers", "MOHNationalityCode");
            DropColumn("dbo.Customers", "MOHNationality");
            DropColumn("dbo.Customers", "MOHEthnicCode");
            DropColumn("dbo.Customers", "MOHEthnic");
            DropColumn("dbo.Customers", "MOHJobCode");
            DropColumn("dbo.Customers", "MOHJob");
        }
    }
}
