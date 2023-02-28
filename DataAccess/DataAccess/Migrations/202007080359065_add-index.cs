namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addindex : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "PID", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Customers", "Fullname", c => c.String(maxLength: 255));
            AlterColumn("dbo.Customers", "Phone", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.EDs", "ATSScale", c => c.String(maxLength: 50));
            AlterColumn("dbo.EDs", "VisitCode", c => c.String(maxLength: 255, unicode: false));
            AlterColumn("dbo.EDs", "RecordCode", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.ICD10", "Code", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.MasterDatas", "Code", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.MasterDatas", "Group", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.MasterDatas", "Form", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.OPDs", "VisitCode", c => c.String(maxLength: 255, unicode: false));
            AlterColumn("dbo.OPDs", "RecordCode", c => c.String(maxLength: 100, unicode: false));
            CreateIndex("dbo.Customers", "PID");
            CreateIndex("dbo.Customers", "Fullname");
            CreateIndex("dbo.Customers", "Phone");
            CreateIndex("dbo.EDs", "ATSScale");
            CreateIndex("dbo.EDs", "AdmittedDate");
            CreateIndex("dbo.EDs", "VisitCode");
            CreateIndex("dbo.EDs", "RecordCode");
            CreateIndex("dbo.ICD10", "Code");
            CreateIndex("dbo.MasterDatas", "Code");
            CreateIndex("dbo.MasterDatas", "Group");
            CreateIndex("dbo.MasterDatas", "Form");
            CreateIndex("dbo.MasterDatas", "Order");
            CreateIndex("dbo.OPDs", "AdmittedDate");
            CreateIndex("dbo.OPDs", "VisitCode");
            CreateIndex("dbo.OPDs", "RecordCode");
        }
        
        public override void Down()
        {
            DropIndex("dbo.OPDs", new[] { "RecordCode" });
            DropIndex("dbo.OPDs", new[] { "VisitCode" });
            DropIndex("dbo.OPDs", new[] { "AdmittedDate" });
            DropIndex("dbo.MasterDatas", new[] { "Order" });
            DropIndex("dbo.MasterDatas", new[] { "Form" });
            DropIndex("dbo.MasterDatas", new[] { "Group" });
            DropIndex("dbo.MasterDatas", new[] { "Code" });
            DropIndex("dbo.ICD10", new[] { "Code" });
            DropIndex("dbo.EDs", new[] { "RecordCode" });
            DropIndex("dbo.EDs", new[] { "VisitCode" });
            DropIndex("dbo.EDs", new[] { "AdmittedDate" });
            DropIndex("dbo.EDs", new[] { "ATSScale" });
            DropIndex("dbo.Customers", new[] { "Phone" });
            DropIndex("dbo.Customers", new[] { "Fullname" });
            DropIndex("dbo.Customers", new[] { "PID" });
            AlterColumn("dbo.OPDs", "RecordCode", c => c.String());
            AlterColumn("dbo.OPDs", "VisitCode", c => c.String());
            AlterColumn("dbo.MasterDatas", "Form", c => c.String());
            AlterColumn("dbo.MasterDatas", "Group", c => c.String());
            AlterColumn("dbo.MasterDatas", "Code", c => c.String());
            AlterColumn("dbo.ICD10", "Code", c => c.String());
            AlterColumn("dbo.EDs", "RecordCode", c => c.String());
            AlterColumn("dbo.EDs", "VisitCode", c => c.String());
            AlterColumn("dbo.EDs", "ATSScale", c => c.String());
            AlterColumn("dbo.Customers", "Phone", c => c.String());
            AlterColumn("dbo.Customers", "Fullname", c => c.String());
            AlterColumn("dbo.Customers", "PID", c => c.String());
        }
    }
}
