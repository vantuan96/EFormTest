namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_ed_status : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CustomerStatus", newName: "EDStatus");
            DropForeignKey("dbo.Customers", "CustomerStatusId", "dbo.CustomerStatus");
            DropIndex("dbo.Customers", new[] { "CustomerStatusId" });
            RenameColumn(table: "dbo.EDs", name: "CustomerStatusId", newName: "EDStatusId");
            RenameIndex(table: "dbo.EDs", name: "IX_CustomerStatusId", newName: "IX_EDStatusId");
            AddColumn("dbo.EDs", "MetDoctor", c => c.Boolean(nullable: false));
            AddColumn("dbo.EDs", "ATSScale", c => c.String());
            DropColumn("dbo.Customers", "MetDoctor");
            DropColumn("dbo.Customers", "ATSScale");
            DropColumn("dbo.Customers", "AdmittedDate");
            DropColumn("dbo.Customers", "CustomerStatusId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "CustomerStatusId", c => c.Guid());
            AddColumn("dbo.Customers", "AdmittedDate", c => c.DateTime());
            AddColumn("dbo.Customers", "ATSScale", c => c.String());
            AddColumn("dbo.Customers", "MetDoctor", c => c.Boolean(nullable: false));
            DropColumn("dbo.EDs", "ATSScale");
            DropColumn("dbo.EDs", "MetDoctor");
            RenameIndex(table: "dbo.EDs", name: "IX_EDStatusId", newName: "IX_CustomerStatusId");
            RenameColumn(table: "dbo.EDs", name: "EDStatusId", newName: "CustomerStatusId");
            CreateIndex("dbo.Customers", "CustomerStatusId");
            AddForeignKey("dbo.Customers", "CustomerStatusId", "dbo.CustomerStatus", "Id");
            RenameTable(name: "dbo.EDStatus", newName: "CustomerStatus");
        }
    }
}
