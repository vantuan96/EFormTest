namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addversionpreceduresummary : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EIOProcedureSummaries", "Version", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EIOProcedureSummaries", "Version");
        }
    }
}
