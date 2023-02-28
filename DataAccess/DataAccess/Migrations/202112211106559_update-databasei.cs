namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedatabasei : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServiceGroups", "HISParentId", c => c.Guid());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ServiceGroups", "HISParentId");
        }
    }
}
