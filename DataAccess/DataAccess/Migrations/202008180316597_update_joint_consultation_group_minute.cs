namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_joint_consultation_group_minute : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EIOJointConsultationGroupMinutes", "DoctorId", c => c.Guid());
            CreateIndex("dbo.EIOJointConsultationGroupMinutes", "DoctorId");
            AddForeignKey("dbo.EIOJointConsultationGroupMinutes", "DoctorId", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EIOJointConsultationGroupMinutes", "DoctorId", "dbo.Users");
            DropIndex("dbo.EIOJointConsultationGroupMinutes", new[] { "DoctorId" });
            DropColumn("dbo.EIOJointConsultationGroupMinutes", "DoctorId");
        }
    }
}
