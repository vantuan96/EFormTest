namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatespecialty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Specialties", "HeadCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Specialties", "HeadCode");
        }
    }
}
