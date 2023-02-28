namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateprescripttbl2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PresciptionRoundInfoModels", "Round", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PresciptionRoundInfoModels", "Round", c => c.Int(nullable: false));
        }
    }
}
