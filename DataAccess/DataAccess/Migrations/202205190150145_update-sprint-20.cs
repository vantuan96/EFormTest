namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatesprint20 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IPDHandOverCheckLists", "IsUseHandOverCheckList", c => c.Boolean(nullable: false));
            AddColumn("dbo.IPDThrombosisRiskFactorAssessments", "FormCode", c => c.String());
            AddColumn("dbo.TableDatas", "Order", c => c.Int(nullable: false, identity: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TableDatas", "Order");
            DropColumn("dbo.IPDThrombosisRiskFactorAssessments", "FormCode");
            DropColumn("dbo.IPDHandOverCheckLists", "IsUseHandOverCheckList");
        }
    }
}
