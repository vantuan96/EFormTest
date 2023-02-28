namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateIPD_BRADEN_SCALE : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IPD_BRADEN_SCALE", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.IPD_BRADEN_SCALE", "Discriminator");
        }
    }
}
