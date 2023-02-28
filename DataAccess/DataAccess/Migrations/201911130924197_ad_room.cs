namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ad_room : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTime(),
                        DeletedBy = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.EmergencyTriageRecords", "TriageDateTime", c => c.DateTime());
            AddColumn("dbo.EmergencyTriageRecords", "RoomID", c => c.Guid());
            CreateIndex("dbo.EmergencyTriageRecords", "RoomID");
            AddForeignKey("dbo.EmergencyTriageRecords", "RoomID", "dbo.Rooms", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmergencyTriageRecords", "RoomID", "dbo.Rooms");
            DropIndex("dbo.EmergencyTriageRecords", new[] { "RoomID" });
            DropColumn("dbo.EmergencyTriageRecords", "RoomID");
            DropColumn("dbo.EmergencyTriageRecords", "TriageDateTime");
            DropTable("dbo.Rooms");
        }
    }
}
