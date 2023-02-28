namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createnewtableConfirmDischarge : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IPDConfirmDischarges",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VisitId = c.Guid(),
                        IsSignToConfirm = c.Boolean(nullable: false),
                        ImageUrl = c.String(),
                        Witness = c.String(),
                        DoctorUsername = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IPDs", t => t.VisitId)
                .Index(t => t.VisitId);
            
        }
        
        public override void Down()
        {
           
        }
    }
}
