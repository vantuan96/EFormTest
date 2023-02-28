namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateeoctables2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EOCs", "LastUpdatedAtByDoctor", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EOCs", "LastUpdatedAtByDoctor");
        }
    }
}
