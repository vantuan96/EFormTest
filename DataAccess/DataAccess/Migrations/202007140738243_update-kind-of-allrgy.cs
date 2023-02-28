namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatekindofallrgy : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "KindOfAllergy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "KindOfAllergy");
        }
    }
}
