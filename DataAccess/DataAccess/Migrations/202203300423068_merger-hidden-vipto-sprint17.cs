namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mergerhiddenviptosprint17 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EDs", "UnlockFor", c => c.String());
            AddColumn("dbo.EOCs", "UnlockFor", c => c.String());
            AddColumn("dbo.IPDs", "UnlockFor", c => c.String());
            AddColumn("dbo.OPDs", "UnlockFor", c => c.String());
            AddColumn("dbo.UnlockVips", "GroupId", c => c.Guid(nullable: false));
            AddColumn("dbo.UnlockVips", "Username", c => c.String());
            AddColumn("dbo.UnlockVips", "Note", c => c.String());
            AddColumn("dbo.UnlockVips", "Files", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UnlockVips", "Files");
            DropColumn("dbo.UnlockVips", "Note");
            DropColumn("dbo.UnlockVips", "Username");
            DropColumn("dbo.UnlockVips", "GroupId");
            DropColumn("dbo.OPDs", "UnlockFor");
            DropColumn("dbo.IPDs", "UnlockFor");
            DropColumn("dbo.EOCs", "UnlockFor");
            DropColumn("dbo.EDs", "UnlockFor");
        }
    }
}
