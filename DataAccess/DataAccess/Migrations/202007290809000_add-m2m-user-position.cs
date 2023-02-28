namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addm2muserposition : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UserPositions", newName: "Positions");
            CreateTable(
                "dbo.PositionUsers",
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
                        UserId = c.Guid(nullable: false),
                        PositionId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Positions", t => t.PositionId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.PositionId);
            
            AddColumn("dbo.OPDProcedureSummaries", "HeadOfDepartmentTime", c => c.DateTime());
            AddColumn("dbo.OPDProcedureSummaries", "HeadOfDepartmentId", c => c.Guid());
            AddColumn("dbo.OPDProcedureSummaries", "DirectorTime", c => c.DateTime());
            AddColumn("dbo.OPDProcedureSummaries", "DirectorId", c => c.Guid());
            CreateIndex("dbo.OPDProcedureSummaries", "HeadOfDepartmentId");
            CreateIndex("dbo.OPDProcedureSummaries", "DirectorId");
            AddForeignKey("dbo.OPDProcedureSummaries", "DirectorId", "dbo.Users", "Id");
            AddForeignKey("dbo.OPDProcedureSummaries", "HeadOfDepartmentId", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PositionUsers", "UserId", "dbo.Users");
            DropForeignKey("dbo.PositionUsers", "PositionId", "dbo.Positions");
            DropForeignKey("dbo.OPDProcedureSummaries", "HeadOfDepartmentId", "dbo.Users");
            DropForeignKey("dbo.OPDProcedureSummaries", "DirectorId", "dbo.Users");
            DropIndex("dbo.PositionUsers", new[] { "PositionId" });
            DropIndex("dbo.PositionUsers", new[] { "UserId" });
            DropIndex("dbo.OPDProcedureSummaries", new[] { "DirectorId" });
            DropIndex("dbo.OPDProcedureSummaries", new[] { "HeadOfDepartmentId" });
            DropColumn("dbo.OPDProcedureSummaries", "DirectorId");
            DropColumn("dbo.OPDProcedureSummaries", "DirectorTime");
            DropColumn("dbo.OPDProcedureSummaries", "HeadOfDepartmentId");
            DropColumn("dbo.OPDProcedureSummaries", "HeadOfDepartmentTime");
            DropTable("dbo.PositionUsers");
            RenameTable(name: "dbo.Positions", newName: "UserPositions");
        }
    }
}
