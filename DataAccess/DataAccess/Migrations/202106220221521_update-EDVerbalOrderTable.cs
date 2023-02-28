namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateEDVerbalOrderTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EDVerbalOrders", "Version", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EDVerbalOrders", "Version", c => c.String());
        }
    }
}
