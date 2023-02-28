namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adduserspecialty : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserSpecialties",
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
                        UserId = c.Guid(),
                        SpecialtyId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Specialties", t => t.SpecialtyId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.SpecialtyId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSpecialties", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserSpecialties", "SpecialtyId", "dbo.Specialties");
            DropIndex("dbo.UserSpecialties", new[] { "SpecialtyId" });
            DropIndex("dbo.UserSpecialties", new[] { "UserId" });
            DropTable("dbo.UserSpecialties");
        }
    }
}
