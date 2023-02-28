namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_gender : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "Gender", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "Gender", c => c.String());
        }
    }
}
