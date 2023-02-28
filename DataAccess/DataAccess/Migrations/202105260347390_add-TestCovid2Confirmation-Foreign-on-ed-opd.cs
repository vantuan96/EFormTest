namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTestCovid2ConfirmationForeignonedopd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EDs", "EIOTestCovid2ConfirmationId", c => c.Guid());
            AddColumn("dbo.OPDs", "EIOTestCovid2ConfirmationId", c => c.Guid());
            CreateIndex("dbo.EDs", "EIOTestCovid2ConfirmationId");
            CreateIndex("dbo.OPDs", "EIOTestCovid2ConfirmationId");
            AddForeignKey("dbo.EDs", "EIOTestCovid2ConfirmationId", "dbo.EIOTestCovid2Confirmation", "Id");
            AddForeignKey("dbo.OPDs", "EIOTestCovid2ConfirmationId", "dbo.EIOTestCovid2Confirmation", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OPDs", "EIOTestCovid2ConfirmationId", "dbo.EIOTestCovid2Confirmation");
            DropForeignKey("dbo.EDs", "EIOTestCovid2ConfirmationId", "dbo.EIOTestCovid2Confirmation");
            DropIndex("dbo.OPDs", new[] { "EIOTestCovid2ConfirmationId" });
            DropIndex("dbo.EDs", new[] { "EIOTestCovid2ConfirmationId" });
            DropColumn("dbo.OPDs", "EIOTestCovid2ConfirmationId");
            DropColumn("dbo.EDs", "EIOTestCovid2ConfirmationId");
        }
    }
}
