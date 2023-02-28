namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcoltoIPDMedicalRecords22 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IPDMedicalRecords", "Version", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.IPDMedicalRecords", "Version");
        }
    }
}
