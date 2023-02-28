namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateprescripttbl : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PresciptionRoundInfoModels", "FromDate", c => c.DateTime());
            AlterColumn("dbo.PresciptionRoundInfoModels", "ToDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PresciptionRoundInfoModels", "ToDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PresciptionRoundInfoModels", "FromDate", c => c.DateTime(nullable: false));
        }
    }
}
