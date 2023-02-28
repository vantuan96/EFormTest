namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_field_datatype_initassementfornewborn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IPDInitialAssessmentForNewborns", "DataType", c => c.String());
           
        }

        public override void Down()
        {
            DropColumn("dbo.IPDInitialAssessmentForNewborns", "DataType");
           
        }
    }
}
