namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatepatientfamilyeducationform : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PatientAndFamilyEducationContents", "CreatedInfo", c => c.String());
            AddColumn("dbo.PatientAndFamilyEducationContents", "UpdatedInfo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PatientAndFamilyEducationContents", "UpdatedInfo");
            DropColumn("dbo.PatientAndFamilyEducationContents", "CreatedInfo");
        }
    }
}
