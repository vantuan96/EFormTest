namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIsDoctorChangeForm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OPDOutpatientExaminationNotes", "IsDoctorChangeForm", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OPDOutpatientExaminationNotes", "IsDoctorChangeForm");
        }
    }
}
