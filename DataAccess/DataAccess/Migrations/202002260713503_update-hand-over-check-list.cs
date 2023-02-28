namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatehandoverchecklist : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.OPDs", name: "OPDHandOverCheckList_Id", newName: "OPDHandOverCheckListId");
            RenameIndex(table: "dbo.OPDs", name: "IX_OPDHandOverCheckList_Id", newName: "IX_OPDHandOverCheckListId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.OPDs", name: "IX_OPDHandOverCheckListId", newName: "IX_OPDHandOverCheckList_Id");
            RenameColumn(table: "dbo.OPDs", name: "OPDHandOverCheckListId", newName: "OPDHandOverCheckList_Id");
        }
    }
}
