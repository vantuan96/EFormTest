namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtransferid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EDs", "IsTransfer", c => c.Boolean(nullable: false));
            AddColumn("dbo.EDs", "TransferFromId", c => c.Guid());
            AddColumn("dbo.OPDs", "IsTransfer", c => c.Boolean(nullable: false));
            AddColumn("dbo.OPDs", "TransferFromId", c => c.Guid());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OPDs", "TransferFromId");
            DropColumn("dbo.OPDs", "IsTransfer");
            DropColumn("dbo.EDs", "TransferFromId");
            DropColumn("dbo.EDs", "IsTransfer");
        }
    }
}
