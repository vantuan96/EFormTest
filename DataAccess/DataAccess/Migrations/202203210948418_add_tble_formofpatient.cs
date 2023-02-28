namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_tble_formofpatient : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FormOfPatients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ViName = c.String(nullable: false),
                        EnName = c.String(nullable: false),
                        TypeName = c.String(nullable: false),
                        Area = c.String(nullable: false),
                        ViStatusPatient = c.String(),
                        EnStatusPatient = c.String(),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
           
        }
        
        public override void Down()
        {
           
            DropTable("dbo.FormOfPatients");
        }
    }
}
