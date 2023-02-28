namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delete_tableinitass : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.IPDSiblings", "Gender", c => c.String());
            // DropTable("dbo.IPDApgars");
            // DropTable("dbo.IPDAssesseds");
            DropTable("dbo.IPDDeliveryMethods");
            // DropTable("dbo.IPDInitialAssessmentForPediatricInpatients");
            DropTable("dbo.IPDLabours");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.IPDLabours",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VisitId = c.Guid(nullable: false),
                        Stage = c.String(),
                        Content = c.String(),
                        Order = c.Int(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.IPDInitialAssessmentForPediatricInpatients",
            //    c => new
            //        {
            //            Id = c.Guid(nullable: false),
            //            VisitId = c.Guid(),
            //            RoomId = c.String(),
            //            IsDeleted = c.Boolean(nullable: false),
            //            DeletedBy = c.String(),
            //            DeletedAt = c.DateTime(),
            //            CreatedBy = c.String(),
            //            CreatedAt = c.DateTime(),
            //            UpdatedBy = c.String(),
            //            UpdatedAt = c.DateTime(),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IPDDeliveryMethods",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VisitId = c.Guid(nullable: false),
                        Year = c.String(),
                        Name = c.String(),
                        Order = c.Int(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.IPDAssesseds",
            //    c => new
            //        {
            //            Id = c.Guid(nullable: false),
            //            VisitId = c.Guid(nullable: false),
            //            StaffName = c.String(),
            //            Time = c.String(),
            //            IntervalsFromTimeOfBirth = c.String(),
            //            PositionOfBaby = c.String(),
            //            Colour = c.String(),
            //            HeartRate = c.String(),
            //            Respirations = c.String(),
            //            Temperature = c.String(),
            //            Tone = c.String(),
            //            Activity = c.String(),
            //            FeedingCode = c.String(),
            //            Location = c.String(),
            //            Note = c.String(),
            //            Order = c.Int(nullable: false, identity: true),
            //            IsDeleted = c.Boolean(nullable: false),
            //            DeletedBy = c.String(),
            //            DeletedAt = c.DateTime(),
            //            CreatedBy = c.String(),
            //            CreatedAt = c.DateTime(),
            //            UpdatedBy = c.String(),
            //            UpdatedAt = c.DateTime(),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.IPDApgars",
            //    c => new
            //        {
            //            Id = c.Guid(nullable: false),
            //            VisitId = c.Guid(nullable: false),
            //            ApgarScore = c.String(),
            //            Total = c.String(),
            //            SkinColor = c.String(),
            //            Respiration = c.String(),
            //            Reflex = c.String(),
            //            MuscleTone = c.String(),
            //            HeartRate = c.String(),
            //            Order = c.Int(nullable: false, identity: true),
            //            IsDeleted = c.Boolean(nullable: false),
            //            DeletedBy = c.String(),
            //            DeletedAt = c.DateTime(),
            //            CreatedBy = c.String(),
            //            CreatedAt = c.DateTime(),
            //            UpdatedBy = c.String(),
            //            UpdatedAt = c.DateTime(),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.IPDSiblings", "Gender", c => c.Int(nullable: false));
        }
    }
}
