namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIsAuthorizedDoctorChangeForm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OPDOutpatientExaminationNotes", "IsAuthorizeDoctorChangeForm", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OPDOutpatientExaminationNotes", "IsAuthorizeDoctorChangeForm");
        }
    }
}
