namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mergrdbtoprod : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IPD_BRADEN_SCALE",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VISIT_ID = c.Guid(),
                        TRANS_DATE = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.EIOMortalityReports", "ICD10", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EIOMortalityReports", "ICD10");
            DropTable("dbo.IPD_BRADEN_SCALE");
        }
    }
}
