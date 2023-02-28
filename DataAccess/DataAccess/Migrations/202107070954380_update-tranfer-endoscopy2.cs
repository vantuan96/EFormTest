namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetranferendoscopy2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EOCs", "CurrentNurseId", "dbo.Users");
            DropIndex("dbo.EOCs", new[] { "CurrentNurseId" });
            RenameColumn(table: "dbo.EOCs", name: "CurrentDoctorId", newName: "PrimaryNurseId");
            RenameIndex(table: "dbo.EOCs", name: "IX_CurrentDoctorId", newName: "IX_PrimaryNurseId");
            DropColumn("dbo.EOCs", "CurrentNurseId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EOCs", "CurrentNurseId", c => c.Guid());
            RenameIndex(table: "dbo.EOCs", name: "IX_PrimaryNurseId", newName: "IX_CurrentDoctorId");
            RenameColumn(table: "dbo.EOCs", name: "PrimaryNurseId", newName: "CurrentDoctorId");
            CreateIndex("dbo.EOCs", "CurrentNurseId");
            AddForeignKey("dbo.EOCs", "CurrentNurseId", "dbo.Users", "Id");
        }
    }
}
