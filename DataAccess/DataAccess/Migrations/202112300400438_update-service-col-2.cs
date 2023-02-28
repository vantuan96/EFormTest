namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateservicecol2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Services", "RootServiceGroupCode", c => c.String());
            AddColumn("dbo.Services", "RootServiceGroupId", c => c.Guid());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Services", "RootServiceGroupId");
            DropColumn("dbo.Services", "RootServiceGroupCode");
        }
    }
}
