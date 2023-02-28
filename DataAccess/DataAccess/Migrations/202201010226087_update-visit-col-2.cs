namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatevisitcol2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EDs", "IsEhos", c => c.Boolean());
            AddColumn("dbo.EOCs", "IsEhos", c => c.Boolean());
            AddColumn("dbo.IPDs", "IsEhos", c => c.Boolean());
            AddColumn("dbo.OPDs", "IsEhos", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OPDs", "IsEhos");
            DropColumn("dbo.IPDs", "IsEhos");
            DropColumn("dbo.EOCs", "IsEhos");
            DropColumn("dbo.EDs", "IsEhos");
        }
    }
}
