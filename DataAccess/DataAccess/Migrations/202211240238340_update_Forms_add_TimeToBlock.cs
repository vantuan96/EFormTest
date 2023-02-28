namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_Forms_add_TimeToBlock : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Forms", "TimeToLockForm", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Forms", "TimeToLockForm");
        }
    }
}
