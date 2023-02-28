namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateeocpk : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EOCs", "AuthorizedDoctorId", c => c.Guid());
            AddColumn("dbo.EOCOutpatientExaminationNotes", "IsAuthorizeDoctorChangeForm", c => c.Boolean(nullable: false));
            AddColumn("dbo.EOCOutpatientExaminationNotes", "IsDoctorChangeForm", c => c.Boolean(nullable: false));
            CreateIndex("dbo.EOCs", "AuthorizedDoctorId");
            AddForeignKey("dbo.EOCs", "AuthorizedDoctorId", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EOCs", "AuthorizedDoctorId", "dbo.Users");
            DropIndex("dbo.EOCs", new[] { "AuthorizedDoctorId" });
            DropColumn("dbo.EOCOutpatientExaminationNotes", "IsDoctorChangeForm");
            DropColumn("dbo.EOCOutpatientExaminationNotes", "IsAuthorizeDoctorChangeForm");
            DropColumn("dbo.EOCs", "AuthorizedDoctorId");
        }
    }
}
