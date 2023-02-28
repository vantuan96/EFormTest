namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addEFormVisitId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EDs", "EFormVisitId", c => c.Guid());
            AddColumn("dbo.IPDs", "EFormVisitId", c => c.Guid());
            AddColumn("dbo.OPDs", "EFormVisitId", c => c.Guid());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OPDs", "EFormVisitId");
            DropColumn("dbo.IPDs", "EFormVisitId");
            DropColumn("dbo.EDs", "EFormVisitId");
        }
    }
}
