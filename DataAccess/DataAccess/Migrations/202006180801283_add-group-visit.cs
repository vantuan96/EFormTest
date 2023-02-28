namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addgroupvisit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OPDs", "GroupId", c => c.Guid());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OPDs", "GroupId");
        }
    }
}
