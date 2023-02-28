namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateaddcolumnDoctorFullNameIPDConfirmDischare : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IPDConfirmDischarges", "DoctorFullName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.IPDConfirmDischarges", "DoctorFullName");
        }
    }
}
