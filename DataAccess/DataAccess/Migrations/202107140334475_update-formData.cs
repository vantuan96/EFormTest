namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateformData : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FormDatas", "VisitType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FormDatas", "VisitType");
        }
    }
}
