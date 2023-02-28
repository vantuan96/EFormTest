namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class trssttbl : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.IPD_BRADEN_SCALE", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.IPD_BRADEN_SCALE", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
    }
}
