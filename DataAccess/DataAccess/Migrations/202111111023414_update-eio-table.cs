namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateeiotable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EIOForms", "ConfirmBy", c => c.String());
            AddColumn("dbo.EIOForms", "ConfirmAt", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EIOForms", "ConfirmAt");
            DropColumn("dbo.EIOForms", "ConfirmBy");
        }
    }
}
