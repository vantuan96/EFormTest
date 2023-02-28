namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateedmodel4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EDs", "CovidRiskGroup", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EDs", "CovidRiskGroup");
        }
    }
}
