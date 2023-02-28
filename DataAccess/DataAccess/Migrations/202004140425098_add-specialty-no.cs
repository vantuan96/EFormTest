namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addspecialtyno : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Specialties", "SpecialtyNo", c => c.Int(nullable: false, identity: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Specialties", "SpecialtyNo");
        }
    }
}
